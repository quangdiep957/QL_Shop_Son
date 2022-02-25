using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_SHOPSON
{
    public partial class THONGKE : Form
    {
        Connect con = new Connect();
        public THONGKE()
        {
            InitializeComponent();
            getData();
            getDatacb();
        }
        public void getData()
        {
            try
            {
                string query = " SELECT  HD.TENHD,HD.NGAYLAP,HD.SOLUONG,SP.TENSP,HD.THANHTIEN,SP.GIATIEN,SP.MAMAU,SP.MANCC FROM SANPHAM SP, HOADON HD WHERE SP.MASP = HD.MASP ORDER BY THANHTIEN DESC";
                DataSet ds = con.getData(query, "THONGKE", null);

                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = ds.Tables["THONGKE"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        public void getDatatop()
        {
            try
            {
                string query = " SELECT top(3)  HD.TENHD,HD.NGAYLAP,HD.SOLUONG,SP.TENSP,HD.THANHTIEN,SP.GIATIEN,SP.MAMAU,SP.MANCC FROM SANPHAM SP, HOADON HD WHERE SP.MASP = HD.MASP ORDER BY THANHTIEN desc";
                DataSet ds = con.getData(query, "THONGKE", null);

                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = ds.Tables["THONGKE"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            getDatatop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            tinhtong();
            tinhsl();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getdate();
        }
        public void getDatacb()
        {
            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);

            try
            {
                string query1 = "select NGAYLAP from HOADON";
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(query1, conn);
                da.Fill(ds, "HOADON");
                da.Fill(ds1, "HOADON");
                comboBox1.DataSource = ds.Tables["HOADON"];
                comboBox1.DisplayMember = "NGAYLAP";
                comboBox1.ValueMember = "NGAYLAP";
                comboBox2.DataSource = ds1.Tables["HOADON"];
                comboBox2.DisplayMember = "NGAYLAP";
                comboBox2.ValueMember = "NGAYLAP";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }
        public void tinhtong()
        {
            int tien = dataGridView1.Rows.Count;
            decimal thanhtien = 0;
            for (int i = 0; i < tien - 1; i++)
            {
                thanhtien += decimal.Parse(dataGridView1.Rows[i].Cells["TONGTIEN"].Value.ToString());
                
            }
            textBox2.Text = thanhtien.ToString();
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            decimal value = decimal.Parse(textBox2.Text, System.Globalization.NumberStyles.AllowThousands);
            textBox2.Text = string.Format(culture, "{0:N0}", value);
            textBox2.Select(textBox2.Text.Length, 0);
        }
        public void tinhsl()
        {
            int sum = dataGridView1.Rows.Count;
           decimal soluong = 0;
            for (int i = 0; i < sum - 1; i++)
            {
                soluong += decimal.Parse(dataGridView1.Rows[i].Cells["SOLUONG"].Value.ToString());
            }
            textBox1.Text = soluong.ToString();
        }
        public void getdate()
        {
            try
            {
                string ngayin = comboBox1.SelectedValue.ToString().Trim();
                string ngayout = comboBox2.SelectedValue.ToString().Trim();
                string query = "SELECT  HD.TENHD,HD.NGAYLAP,HD.SOLUONG,SP.TENSP,HD.TONGTIEN,SP.GIABAN,SP.MAMAU,SP.MANCC FROM SANPHAM SP, HOADON HD, CT_HOADON CTHD WHERE SP.MASP = CTHD.MASP AND HD.MAHD = CTHD.MAHD AND NGAYLAP BETWEEN '12/24/2012' AND '12/24/2021' ORDER BY TONGTIEN DESC ";
                DataSet ds = con.getData(query, "THONGKE", null);

                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = ds.Tables["THONGKE"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
