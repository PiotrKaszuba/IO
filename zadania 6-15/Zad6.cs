using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
namespace IO2_Lab
{
    class Zad6
    {

        public static void call(IAsyncResult state)
        {
          
            FileStream fs = (FileStream)(((object[])(state.AsyncState))[0]);
            byte[] buff = (byte[])(((object[])(state.AsyncState))[1]);
            AutoResetEvent are = (AutoResetEvent)(((object[])(state.AsyncState))[2]);

            int ile = fs.EndRead(state);


            fs.Close();
            Console.WriteLine(Encoding.ASCII.GetString(buff,0,ile));
            are.Set();

        }


        public Zad6()
        {
            FileStream fs = new FileStream("plik.txt", FileMode.Open);
            byte[] buffer = new byte[1024];
            AutoResetEvent are = new AutoResetEvent(false);
            fs.BeginRead(buffer, 0, buffer.Length, call, new object[] { fs, buffer, are });


            are.WaitOne();


        }
    }
}
