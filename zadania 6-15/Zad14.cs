using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace IO2_Lab
{
    class Zad14
    {

        public async Task<string> get_web(string address)
        {
            WebClient cli = new WebClient();
            Console.WriteLine("thr: " + Thread.CurrentThread.ManagedThreadId);
            string xmlcontent = await cli.DownloadStringTaskAsync(new Uri(address));
           // Console.WriteLine("thread: " + Thread.CurrentThread.ManagedThreadId);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlcontent);

            StringWriter sw = new StringWriter();
            XmlTextWriter xtw = new XmlTextWriter(sw);
            doc.WriteTo(xtw);


            return sw.ToString();
        }

        public Zad14()
        {


            string doc = get_web("http://www.feedforall.com/sample.xml").Result;


           

            //Console.WriteLine("thr: " + Thread.CurrentThread.ManagedThreadId);

        

            //Console.WriteLine("thr: " + Thread.CurrentThread.ManagedThreadId);
            //Console.WriteLine("after");
        }
    }
}
