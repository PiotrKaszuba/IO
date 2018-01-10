using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO2_Lab
{
    class Zad10
    {

        private void write_matrix(int[,] mat, int size)
        {
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(mat[i, j] + " ");

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private void HANDLE(
              object sender,
              MatMulCompletedEventArgs e)
        {
            lock (this)
            {
                try {
                    Console.WriteLine("Zakonczono operacje o identyfikatorze: " + (int)e.UserState);
                    if (e.Error != null) Console.WriteLine("Wystapil blad " + e.Error.Message);
                    
                   
                    //if (e.Macierz != null)
                        write_matrix(e.Macierz, e.Size);
                }
                catch(Exception f)
                {
                    Console.WriteLine(f.Message);
                }

                Console.WriteLine("----------------------");
            }
        }
        public Zad10()
        {
            MatMulCalculator cal = new MatMulCalculator();
            cal.MatMulCompleted += new MatMulCalculator.MatMulCompletedEventHandler(HANDLE);

            int n = 12;

            int[,] f = new int[n, n];
            int[,] g = new int[n, n];

            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    f[i, j] = r.Next()%10;
                    g[i, j] = r.Next()%10;
                }
            }
            write_matrix(f, n);
            write_matrix(g, n);


            cal.MatMulAsync(f, g, n, 1000);
            cal.MatMulAsync(f, g, n, 1002);

            cal.MatMulAsync(f, g, n, 1003);
            cal.MatMulAsync(f, g, n, 1004);
            cal.MatMulAsync(f, g, n, 1001);

            cal.CancelAsync(1001);
         


        }
  

    }
}
