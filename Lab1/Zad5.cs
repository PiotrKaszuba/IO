using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
namespace IO
{
    class Zad5
    {
        public AutoResetEvent[] are_tab;
        //public static int length = 10000;
        public static int[] tablica;
        public static int total = 0;
        public static object loc = new object();
        public static Stopwatch x;
        int licz(Object info)
        {
            //x.Start();
            int start = (int)((object[])info)[0];
            int stop = (int)((object[])info)[1];
            int id = (int)((object[])info)[2];
            int suma = 0;

            for (int i = start; i < stop; i++)
            {
                suma += tablica[i];
            }

            lock (loc)
            {
                total += suma;
            }
            are_tab[id].Set();
            return suma;
        }

        public Zad5(int length, int mod, Stopwatch ax){
            
            x = ax;
            //x.Start();
            tablica =  new int[length];
            Random rand = new Random();
            //int mod = 10;

            int all = (int)(length *( (float) (mod - 1) / 2));
            int t = 0;
            for (int i = 0; i < length; i++)
            {
                t = rand.Next()%(mod+1);
                if (all >= t)
                {
                    tablica[i] = t;
                    all -= t;
                }
                else
                {
                    tablica[i] = all;
                    all -= all;
                }
            }
            //x.Stop();
           x.Start();


            int batch_size = length/50;
            int batch_number = (tablica.Length + batch_size - 1) / batch_size;
            //object[] indeksy = new object[batch_number];

            are_tab = new AutoResetEvent[batch_number];
            for (int i = 0; i < batch_number; i++)
            {
                are_tab[i] = new AutoResetEvent(false);
                int top = (i + 1) * batch_size;
                if (top > tablica.Length) top = tablica.Length;

                int copy = i;
                ThreadPool.QueueUserWorkItem(o => { licz(new object[] { copy * batch_size, top, copy }); });
                
            }
            WaitHandle.WaitAll(are_tab);
           x.Stop();
            
            System.Console.WriteLine(total);
            //System.Console.ReadKey();

          


           
           
            
    
        }
    }
}
