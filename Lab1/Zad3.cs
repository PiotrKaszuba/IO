using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
namespace IO
{
    class Zad3
    {

        class Clients
        {

            public static object myobject = new object();

            public static void WriteInColor(String text, ConsoleColor color)
            {
                lock (myobject)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine(text);
                    Console.ResetColor();
                }
            }


            public void begin()
            {

                byte[] buf = new byte[1024];

                TcpClient x = new TcpClient();
                x.Connect("localhost", 1024);
                x.GetStream().Read(buf, 0, 4);


                WriteInColor(""+((int)buf[0]), ConsoleColor.Red);

                string s = "odebrano bajt " + (int)buf[0];

                buf = Encoding.ASCII.GetBytes(s);
                x.GetStream().Write(buf, 0, buf.Length);

            }
        }

        class Servers
        {
            public void begin()
            {
               


                TcpListener x = new TcpListener(1024);

                x.Start();

                int i = 0;


                while (true)
                {
                    

                    byte[] buf = BitConverter.GetBytes(i);
                    
                    TcpClient client = x.AcceptTcpClient();


                    Thread v = new Thread(o => {

                        client.GetStream().Write(buf, 0, buf.Length);

                        buf = new byte[1024];
                        int lnt = client.GetStream().Read(buf, 0, 1024);

                        
                        string result = System.Text.Encoding.UTF8.GetString(buf,0,lnt) ;

                        Clients.WriteInColor(result, ConsoleColor.Green);

                    });


                    v.Start();


                    i++;

                }
            }
        }

        public Zad3()
        {
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
