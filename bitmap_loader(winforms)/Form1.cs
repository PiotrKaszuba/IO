using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_Bitmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async Task<Bitmap> LoadAsync()
        {

            try
            {
                Bitmap bm = new Bitmap(textBox1.Text);
                //LONG LOADING DEMO
                Thread.Sleep(3000);

                return bm;
            }
            catch (Exception x)
            {
                MessageBox.Show("No image with that name");
            }
            return null;

        }


        private async void LoadBitmapAsync()
        {
            
            pictureBox1.Image = await Task.Run(LoadAsync);
          
        }

        private void LoadBitmap()
        {
            try
            {


                Bitmap bm = new Bitmap(textBox1.Text);
                //LONG LOADING DEMO
                Thread.Sleep(3000);
                pictureBox1.Image = bm;
            }
            catch (Exception x)
            {
                MessageBox.Show("No image with that name");
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            LoadBitmapAsync();
        }

      
    }
}
