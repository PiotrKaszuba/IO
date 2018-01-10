using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO2_Lab
{
    class MatMulCalculator
    {
        public delegate void MatMulCompletedEventHandler (object sender, MatMulCompletedEventArgs args);
        private delegate void WorkerEventHandler(int[,] mat1, int[,] mat2, int size, AsyncOperation asyncop);

        public event MatMulCompletedEventHandler MatMulCompleted;
        
        SendOrPostCallback onCompletedCallback;

        private HybridDictionary userStateToLifetime = new HybridDictionary();

        public MatMulCalculator()
        {
            onCompletedCallback = CalculateCompleted;
        }




        public  void CalculateCompleted(object state)
        {
            MatMulCompletedEventArgs e = (MatMulCompletedEventArgs)state;
   

            MatMulCompletedEventHandler handler = MatMulCompleted;

            if (handler != null) handler(this, e);

           
        }

        void Completion(int[,] mat, int size, Exception ex, bool cancelled, AsyncOperation aop)
        {
           

            MatMulCompletedEventArgs e = new MatMulCompletedEventArgs(mat, size, ex, cancelled, aop.UserSuppliedState);

            aop.PostOperationCompleted(onCompletedCallback, e);

            lock (userStateToLifetime.SyncRoot)
            {
                userStateToLifetime.Remove(aop.UserSuppliedState);
            }


        }

        bool TaskCancelled(object taskID)
        {
            lock (userStateToLifetime.SyncRoot)
            {
                return (bool)userStateToLifetime[taskID];
            }
        }

        void CalculateWorker(int[,] mat1, int[,] mat2, int size, AsyncOperation aop)
        {
            int[,] result = null;
            Exception ex = null;


            bool cancelled = TaskCancelled(aop.UserSuppliedState);
            try {
                if (!cancelled)
                {
                    result = MatMul(mat1, mat2, size);
                }
            }
            catch(Exception e)
            {
                ex = e;
            }

            Completion(result, size, ex, cancelled, aop);
            
        }


        int[,] MatMul(int[,] mat1, int[,] mat2, int size)
        {
            int[,] wynik = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int suma = 0;
                    for (int k = 0; k < size; k++)
                    {
                        suma += mat1[i, k] * mat2[k, j];
                    }
                    wynik[i, j] = suma;
                }
            }
            return wynik;
        
        }



        public virtual void MatMulAsync(int[,] mat1, int[,] mat2, int size, object taskID)
        {
            
         
            lock (userStateToLifetime.SyncRoot)
            {
                if (userStateToLifetime.Contains(taskID))
                {
                    throw new ArgumentException(
                        "Task ID parameter must be unique",
                        "taskId");
                }
                userStateToLifetime.Add(taskID, false);
            }

            AsyncOperation aop = AsyncOperationManager.CreateOperation(taskID);

            WorkerEventHandler weh = new WorkerEventHandler(CalculateWorker);

            weh.BeginInvoke(mat1, mat2, size, aop, null,null);
     
            
        }

        public void CancelAsync(object taskID)
        {
            lock (userStateToLifetime.SyncRoot)
            {
                userStateToLifetime[taskID] = true;
            }
        }




    }
}
