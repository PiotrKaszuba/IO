using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO;
using System.ComponentModel;

namespace IO2_Lab
{
       
    class Zad11
    {
        private void progress(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Progress %: " + e.ProgressPercentage);
            if(e.ProgressPercentage.Equals(100)) ((BackgroundWorker)sender).Dispose();


        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            Clients cli = new Clients();
            for (int i = 1; i <= 10; i++)
            {
                cli.begin();
                ((BackgroundWorker)sender).ReportProgress(i*10);
            }

           


        }
        private void completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Komunikacja zakończona");

        }

        private void disposed(object sender, EventArgs e)
        {
            Console.WriteLine("Usuwanie");

        }

        public Zad11()
        {

            BackgroundWorker f = new BackgroundWorker();
            f.DoWork += backgroundWorker1_DoWork;
            f.RunWorkerCompleted += completed;
            f.ProgressChanged += progress;
            f.Disposed += disposed;
            f.WorkerReportsProgress = true;
            f.RunWorkerAsync();
            

           
            Servers serv = new Servers();
            serv.begin();
        }
    }
}
