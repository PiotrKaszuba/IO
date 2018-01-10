using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace IO
{
    class Zad1
    {
        void task(Object state)
        {
            Console.WriteLine("Start");
            int time = (int)((object[])state)[0];
            Thread.Sleep(time);

            Console.WriteLine("Czekano "+ time);
        }

        public Zad1()
        {
           
           
            WaitCallback wc = new WaitCallback(task);

            object[] stateinfo = new object[] { 4000 };

            ThreadPool.QueueUserWorkItem(wc, stateinfo);
            ThreadPool.QueueUserWorkItem(wc, stateinfo);
         
            Thread.Sleep(5000);

        }

    }
}
