using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace IO
{
 
    class Program
    {
        public static void exec_zad_5()
        {
            int length = 200000000;
            int mod = 100;
            Stopwatch x = new Stopwatch();



            x.Reset();


            //x.Start();
            Zad5 f = new Zad5(length, mod, x);

            //x.Stop();

            System.Console.WriteLine(x.Elapsed);
            x.Reset();

            //x.Start();
            AddValues g = new AddValues(length, mod, x);
            //x.Stop();
            System.Console.WriteLine(x.Elapsed);


            System.Console.ReadKey();

        }

        static void Main(string[] args)
        {
            //Zad1 x = new Zad1();
            //Zad2 x = new Zad2();
            //Zad3 x = new Zad3();

            exec_zad_5();
            





            /*
            //ThreadPool.QueueUserWorkItem()
            //Task x = new Task()
            Program p = new Program();
            Action a = new Action(() => { p.wejt(null); });

            Task t = new Task(a);
            object stateinfo;
            WaitCallback wc = new WaitCallback(p.wejt);
           
            stateinfo = (object)4000;
            
            ThreadPool.QueueUserWorkItem(wc, stateinfo);
            ThreadPool.QueueUserWorkItem(wc, stateinfo);
            //t.Start();
            Thread.Sleep(5000);
            */
            
        }
    }
}
