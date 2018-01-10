using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO2_Lab
{
    //TAP
    class Example
    {
        public static async Task OperationTask(object data)
        {
            Console.WriteLine("begin task");
            await Task.Run(() =>
           {
               Console.WriteLine("begin async");
               Thread.Sleep(100);
               Console.WriteLine("end async");

           });

            Console.WriteLine("end task");
        }


        public static async Task<int> GetInt(object data)
        {
            Thread.Sleep(5000);
            return 10;
        }
        public static async Task testm()
        {
            Console.WriteLine("th: " + Thread.CurrentThread.ManagedThreadId);
            int i = await GetInt(0);
            Console.WriteLine("th: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(i);
        }
        public static async Task f()
        {
            Console.WriteLine("th: "+Thread.CurrentThread.ManagedThreadId);
            await testm();
            Console.WriteLine("th: " + Thread.CurrentThread.ManagedThreadId);
        }
        public Example()
        {
            Console.WriteLine("thr: " + Thread.CurrentThread.ManagedThreadId);
            Task ts = f();
            Console.WriteLine("thr: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("stop");

            int test = 1;
            byte[] buffer = new byte[128];
            Console.WriteLine("begin main");
            Task task = OperationTask(buffer);
            Thread.Sleep(test);
            Console.WriteLine("progress main");
            task.Wait();
            Console.WriteLine("end main");
        }
    }


}
