using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO2_Lab
{
    class Zad13
    {
        public bool Z2 { get; private set; }

        public void Zadanie2()
        {
            //ZADANIE 2. ODKOMENTUJ I POPRAW  

            Task.Run(
                () =>
                {
                    Z2 = true;
                });
             
        }
    }
}
