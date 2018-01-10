using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
namespace IO2_Lab
{
    class Zad7
    {


   


        public Zad7()
        {


            FileStream fs = new FileStream("plik.txt", FileMode.Open);
            byte[] buffer = new byte[1024];
            
            
            IAsyncResult iar = fs.BeginRead(buffer, 0, buffer.Length, null, null);
            int ile = fs.EndRead(iar);


            fs.Close();
            Console.WriteLine(Encoding.ASCII.GetString(buffer,0, ile));


            
            
        }
    }

}
