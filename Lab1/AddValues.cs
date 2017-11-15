using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace IO
{
    class AddValues
    {
        public static int[] tablica;
        public static Stopwatch x;
        public static int total = 0;
        public AddValues(int length, int mod, Stopwatch ax)
        {
            
            x = ax;
           // x.Start();
            tablica = new int[length];
            Random rand = new Random();

            int all = (int)(length * ((float)(mod - 1) / 2));
            int t = 0;

            for (int i = 0; i < length; i++)
            {
                t = rand.Next() % (mod + 1);
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

            for (int i = 0; i < length; i++)
            {
                total += tablica[i];

            }
           x.Stop();
            System.Console.WriteLine(total);
        }
    }
}
