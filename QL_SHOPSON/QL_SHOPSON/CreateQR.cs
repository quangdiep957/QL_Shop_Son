using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QL_SHOPSON
{
    public partial class CreateQR : Form
    {
        public CreateQR()
        {

            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string add = textBox1.Text + "," + textBox2.Text;
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;   
            pictureBox1.Image = qrcode.Draw(add, 50);
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PNG|*.png" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                    pictureBox1.Image.Save(sfd.FileName, ImageFormat.Png);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox1.Image = barcode.Draw(textBox1.Text + ", " + textBox2.Text, 50);
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PNG|*.png" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                    pictureBox1.Image.Save(sfd.FileName, ImageFormat.Png);
            }
        }
    }
}
