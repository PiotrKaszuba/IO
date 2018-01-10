using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;

namespace IO
{
    
    public class Clients
    {
       
        public void begin()
        {

            byte[] buf = new byte[1024];

            TcpClient x = new TcpClient();
            x.Connect("localhost", 1024);

            x.GetStream().Read(buf, 0, 4);
           
            Console.WriteLine("" + BitConverter.ToInt32(buf, 0));

         
        }
    }

    public class Servers
    {
        public void begin()
        {
           
            TcpListener x = new TcpListener(1024);
            x.Start();

            int i = 400;

            while (true)
            {
               
                
                byte[] buf = BitConverter.GetBytes(i);

                TcpClient client = x.AcceptTcpClient();
                client.GetStream().Write(buf, 0, buf.Length);

                i++;

            }
        }
    }
    class Zad2
    {
       public Zad2() {
            Clients a = new Clients();
            Clients b = new Clients();
            Servers s = new Servers();
            ThreadPool.QueueUserWorkItem(o => s.begin());
            ThreadPool.QueueUserWorkItem(o => a.begin());

            ThreadPool.QueueUserWorkItem(o => b.begin());
            Thread.Sleep(10000);
          }
    }


}
