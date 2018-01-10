using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO2_Lab
{
    class MatMulCompletedEventArgs : AsyncCompletedEventArgs
    {

        private int[,] macierz;
        private int size;

        public int[,] Macierz
        {
            get
            {
                RaiseExceptionIfNecessary();
                return macierz;
            }
        }

        public int Size
        {
            get
            {
                RaiseExceptionIfNecessary();
                return size;
            }
        }


        public MatMulCompletedEventArgs(int[,] macierz, int size, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
        {
            this.macierz = macierz;
            this.size = size;
          
        }
    }
}
