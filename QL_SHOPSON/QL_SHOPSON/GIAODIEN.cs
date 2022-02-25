using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_SHOPSON
{
    public partial class GIAODIEN : Form
    {
        QL_HD hd = new QL_HD();
        QL_NV nv = new QL_NV();
        QL_SP sp = new QL_SP();
        public GIAODIEN()
        {

            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            sp.Show();
            this.Visible = false;

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            nv.Show();
            this.Visible = false;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            hd.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DANGNHAP gd = new DANGNHAP();
            gd.Show();
            this.Visible = false;
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            THONGKE gd = new THONGKE();
            gd.Show();
            this.Visible = false;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print pr = new Print();
            pr.Show();
            this.Visible = false;
        }
    }
}
