using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace IO2_Lab
{
    class Zad8
    {

        static AutoResetEvent are = new AutoResetEvent(false);
        delegate double funkcja(int args);
        

        public double factorial_recursion(int number)
        {
            if (number == 1)
                return 1;
            else
                return number * factorial_recursion(number - 1);
        }


        public double factorial_WhileLoop(int number)
        {
            double result = 1;
            while (number != 1)
            {
                result = result * number;
                number = number - 1;
            }
            return result;
        }


        public Zad8()
        {

            funkcja[] fact = new funkcja[2];

            
             fact[1] = new funkcja(factorial_WhileLoop);
            fact[0] = new funkcja(factorial_recursion);


            IAsyncResult[] iar = new IAsyncResult[2];
            WaitHandle[] array = new WaitHandle[iar.Length];


            for (int i =0;i<2;i++)
            {
                iar[i] = fact[i].BeginInvoke(75, null, null);
                array[i] = iar[i].AsyncWaitHandle;
            }
          
            
            int ktory = WaitHandle.WaitAny(array);





            Console.WriteLine(


                fact[ktory].EndInvoke(iar[ktory])
                + "\n" +
                fact[ktory].Method.ToString()
                
                
                );

        }

    }
}
