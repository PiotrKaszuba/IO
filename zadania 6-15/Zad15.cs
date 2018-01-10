using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO2_Lab
{
    class ServerLog
    {
        public Queue<string> logi = new Queue<string>();
        public bool writing = false;
        private object lck = new object();
        public void Log(string log)
        {
            lock (logi)
            {
                logi.Enqueue(log);
                if (!writing)
                {
                    writing = true;
                    Task.Run(WriteAsync);
                }
               
            }
            
        }
        private void WriteFunc(string log)
        {
            lock (lck)
            {
                Console.WriteLine("log " + DateTime.Now + ": " + log);
            }
        }
        private async Task WriteAsync()
        {
            string log;
            while(true)
            {
                lock(logi){
                    if (logi.Count > 0)
                    {
                        log = logi.Dequeue();
                    }
                    else break;
                }

                WriteFunc(log);
            }
            
            lock(logi)
            {
                writing = false;
            }
       
        }
    }
    class Zad15
    {
        public static ServerLog log = new ServerLog();
        static object serverlock = new object();
        static async Task serverTask()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                byte[] buffer = new byte[1024];

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                    async (t) =>
                    {
                        int i = t.Result;
                        while (true)
                        {
                            
                            client.GetStream().WriteAsync(buffer, 0, i);

                            string message = Encoding.ASCII.GetString(buffer, 0, i);
                           
                                Console.WriteLine(message);
                                log.Log(message);
                           
                            
                            
                            i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                        }
                    });
            }

        }

        static string get_message(int data)
        {
            return "message from client "+data;
        }

        static void write_message(string message, int data)
        {
            Console.WriteLine("Client " + data+" got message: " + message);
        }

        public static async Task<int> clientTask(int data)
        {

           

            TcpClient x = new TcpClient();
            x.Connect("localhost", 2048);
            bool a = false;
            do
            {
                byte[] buffer = new byte[1024];
                string message = get_message(data);

                if (message.Equals("\\exit")) break;
                byte[] buf = Encoding.ASCII.GetBytes(message);

                x.GetStream().Write(buf, 0, buf.Length);


                int i = x.GetStream().Read(buffer, 0, buffer.Length);


                write_message(Encoding.ASCII.GetString(buffer,0, i), data);


            } while (a);


            return 0;
            


    }


        

        public Zad15()
        {
            Func<object, int> client = ( i) => clientTask((int)i).Result;
            var tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(serverTask));
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(client, i));
            }
            
            Task.WaitAll();
        }



    }
}
