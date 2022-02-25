using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_SHOPSON
{
    public partial class QL_HD : Form
    {
        List<Class1> list = new List<Class1>();
        Connect con = new Connect();

        public string soluong { get; private set; }
        public string masp { get; private set; }
        public string giatien { get; private set; }
        public string mahd { get; private set; }

        public QL_HD()
        {
            InitializeComponent();
            getData();
            getDatacb();
            getDataMSP();
            getimg();

        }
        public void getData()
        {
            try
            {
                string Query = "Select * from HOADON";
                DataSet ds = con.getData(Query, "HOADON", null);

                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = ds.Tables["HOADON"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        public void getDatacb()
        {
            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);

            try
            {
                string query1 = "select TENKH,MAKH from KHACHHANG";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(query1, conn);
                da.Fill(ds, "KHACHHANG");
                comboBox1.DataSource = ds.Tables["KHACHHANG"];
                comboBox1.DisplayMember = "TENKH";
                comboBox1.ValueMember = "MAKH";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }
        public void getDataMSP()
        {
            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);

            try
            {
                string query1 = "select MASP,TENSP,GIANHAP from SANPHAM";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(query1, conn);
                da.Fill(ds, "SANPHAM");
                comboBox2.DataSource = ds.Tables["SANPHAM"];
                comboBox2.DisplayMember = "TENSP";
                comboBox2.ValueMember = "MASP";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex);
            }
        }

        public void getimg()
        {

            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);
            string s = comboBox2.SelectedValue.ToString().Trim();

            string Query = "SELECT ANH FROM SANPHAM WHERE MASP='" + s + "'";


            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Hiển thị dữ liệu lên TEXT
            if (dt.Rows.Count > 0)
            {
                //textBox1.Text = dt.Rows[0]["ANH"].ToString().Trim();
                var image = (Bitmap)Bitmap.FromFile(dt.Rows[0]["ANH"].ToString().Trim());
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            else
            {
                MessageBox.Show("Khoong");
            }


        }
        public void getimg1(PictureBox picname ,string s)
        {

            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);
            //string s = comboBox2.SelectedValue.ToString().Trim();

            string Query = "SELECT ANH FROM SANPHAM WHERE MASP='" + s + "'";


            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Hiển thị dữ liệu lên TEXT
            if (dt.Rows.Count > 0)
            {
                //textBox1.Text = dt.Rows[0]["ANH"].ToString().Trim();
                var image = (Bitmap)Bitmap.FromFile(dt.Rows[0]["ANH"].ToString().Trim());
                picname.Image = image;
                picname.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            else
            {
                MessageBox.Show("Khoong");
            }


        }
        public void clearText()
        {
            txtmhd.Enabled = true;
            txtthd.Enabled = true;

            txttt.Enabled = true;
            //txtmsp.Enabled = true;
            txtmnv.Enabled = true;
            btnthem.Enabled = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;



        }






        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx >= 0)
            {
                txtmhd.Enabled = false;

                btnthem.Enabled = false;
                btnsua.Enabled = true;
                btnxoa.Enabled = true;
                txtmhd.Text = dataGridView1.Rows[idx].Cells["MAHD"].Value.ToString();
                txtthd.Text = dataGridView1.Rows[idx].Cells["TENHD"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[idx].Cells["NGAYLAP"].Value.ToString();
                //  txtmsp.Text = dataGridView1.Rows[idx].Cells["MASP"].Value.ToString();
                txttt.Text = dataGridView1.Rows[idx].Cells["TONGTIEN"].Value.ToString();

                comboBox1.Text = dataGridView1.Rows[idx].Cells["MAKH"].Value.ToString();
                txtsl.Text = dataGridView1.Rows[idx].Cells["SOLUONG"].Value.ToString();
                txtmnv.Text = dataGridView1.Rows[idx].Cells["MANV"].Value.ToString();




            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnthem_Click_1(object sender, EventArgs e)
        {
            string tenhd = txtthd.Text;
            string ngaylap = dateTimePicker1.Value.ToString();
            string msp = comboBox2.SelectedValue.ToString().Trim();
           // string thanhtien = txttt.Text;
            string mnv = txtmnv.Text;
            string sl = txtsl.Text;
            string mkh = comboBox1.SelectedValue.ToString();
            int tongtien = 0;
            int slsp = 0;
            string Query = "insert into HOADON VaLUES( @thd,@ngaylap,@tongtien,@mkh,@sl,@mnv)";

            List<SqlParameter> _params = new List<SqlParameter>();
            for (int i = 0; i < list.Count; i++)
            {
                tongtien += int.Parse(list[i].tongtien);
                slsp += int.Parse(list[i].soluong);
               
            }
            if(txtdiem!=null)
            {
                tongtien = tongtien - int.Parse(txtdiem.Text);

            }
            _params.Add(new SqlParameter("@thd", tenhd));
            _params.Add(new SqlParameter("@ngaylap", ngaylap));
            _params.Add(new SqlParameter("@tongtien", tongtien));
            _params.Add(new SqlParameter("@mkh", mkh));
            _params.Add(new SqlParameter("@sl", slsp));
            _params.Add(new SqlParameter("@mnv", mnv));

            con.excute(Query, _params);
            MessageBox.Show("Thêm mới thành công");
            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);
            string s = comboBox2.SelectedValue.ToString().Trim();

            string query2 = "select mahd from hoadon where ngaylap ='" + ngaylap + "' and makh='" + mkh + "'";      
            SqlDataAdapter da = new SqlDataAdapter(query2, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Hiển thị dữ liệu lên TEXT
            if (dt.Rows.Count > 0)
            {
                string mahd= dt.Rows[0]["MAHD"].ToString().Trim();
                for (int i=0;i<list.Count;i++)
                {
                    tongtien += int.Parse(list[i].tongtien);
                    string Query1 = "insert into CT_HOADON(MAHD,SOLUONG,GIATIEN,MASP) values (@mahd,@sl,@tongtien,@msp)";
                    List<SqlParameter> _params1 = new List<SqlParameter>();
                    _params1.Add(new SqlParameter("@mahd",mahd));
                    _params1.Add(new SqlParameter("@sl", list[i].soluong));
                    _params1.Add(new SqlParameter("@tongtien", list[i].giatien));
                    _params1.Add(new SqlParameter("@msp", list[i].masp));
                    con.excute(Query1, _params1);
                }


                taothe(mkh);
            }
            getData();

        }

        private void btnsua_Click_1(object sender, EventArgs e)
        {

            string mhd = txtmhd.Text;
            string tenhd = txtthd.Text;
            string ngaylap = dateTimePicker1.Value.ToString();
            string msp = comboBox2.SelectedValue.ToString().Trim();
            int thanhtien = int.Parse(txttt.Text);
            string mnv = txtmnv.Text;
            string mkh = comboBox1.SelectedValue.ToString();
            string sl = txtsl.Text;

            string query = "Update HOADON set TENHD=@thd,NGAYLAP =@ngaylap ,TONGTIEN= @thanhtien  ,MANV=@mnv,MAKH=@mkh,SOLUONG=@sl where MAHD=@mhd";
            List<SqlParameter> _params = new List<SqlParameter>();
            _params.Add(new SqlParameter("@mhd", mhd));
            _params.Add(new SqlParameter("@thd", tenhd));
            _params.Add(new SqlParameter("@ngaylap", ngaylap));

            _params.Add(new SqlParameter("@thanhtien", thanhtien));
            _params.Add(new SqlParameter("@mnv", mnv));
            _params.Add(new SqlParameter("@mkh", mkh));
            _params.Add(new SqlParameter("@sl", sl));

            con.excute(query, _params);

            MessageBox.Show("Sửa thành công");

            getData();


        }

        private void btnxoa_Click_1(object sender, EventArgs e)
        {

            string mhd = txtmhd.Text;
            List<SqlParameter> _params1 = new List<SqlParameter>();
            string kiemtra = "select count (*) from HOADON where MAHD=@tk ";
            _params1.Add(new SqlParameter("@tk", mhd));

            if (con.Kiemtra(kiemtra, _params1) == 1)
            {

                string Query = "Delete from HOADON where MAHD = @mhd";
                List<SqlParameter> _params = new List<SqlParameter>();
                _params.Add(new SqlParameter("@mhd", mhd));
                con.excute(Query, _params);
                MessageBox.Show("Xóa thành công");
                clearText();
                getData();

            }
            else
            {
                MessageBox.Show("Xóa không thành công!");
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string TK = textBox1.Text;


                List<SqlParameter> _params = new List<SqlParameter>();
                _params.Add(new SqlParameter("@TK", textBox1.Text));
                string query = "Select * from HOADON where  TENHD Like '%" + TK + "%' ";
                con.excute(query, _params);
                getData();
                DataSet ds = con.getData(query, "HOADON", null);
                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = ds.Tables["HOADON"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GIAODIEN gd = new GIAODIEN();
            gd.Show();
            this.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);
            string s = comboBox2.SelectedValue.ToString().Trim();

            string Query = "SELECT GIABAN FROM SANPHAM WHERE MASP='" + s + "'";


            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Hiển thị dữ liệu lên TEXT
            if (dt.Rows.Count > 0)
            {
                txtgia.Text = dt.Rows[0]["GIABAN"].ToString().Trim();
            }
            else
            {
                MessageBox.Show("Khoong");

            }

            if (txtgia.Text != "" && txtsl.Text != "")
            {
                double i = double.Parse(txtsl.Text);
                double j = double.Parse(txtgia.Text);
                double kq = i * j;
                txttt.Text = kq.ToString();
            }
            else
            {

                MessageBox.Show("vui lòng nhập số lượng");
            }
        }

        private void QL_HD_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy,hh:mm:ss";

            dateTimePicker1.Value = DateTime.Now;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            getimg();
        }
        private void taothe(string makh)
        {
            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);
            string query2 = "select sum(tongtien) as tong from hoadon where makh='" + makh + "'";

            SqlDataAdapter da = new SqlDataAdapter(query2, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Hiển thị dữ liệu lên TEXT
            if (dt.Rows.Count > 0)
            {
                string tong = dt.Rows[0]["tong"].ToString().Trim();
                int diem = int.Parse(tong) / 100000;
                DateTime aDateTime = DateTime.Now;
                DateTime newTime = aDateTime.AddYears(1);
                string ngaykh = dateTimePicker1.Value.ToString();
                string ngayhethan = newTime.ToString();
                int id_loaithe = 2;
                if (diem >= 1000 && diem < 3000)
                {
                    id_loaithe = 3;
                }
                if (diem >= 3000 && diem < 10000)
                {
                    id_loaithe = 4;
                }
                if (diem > 10000)
                {
                    id_loaithe = 5;
                }
                List<SqlParameter> _params1 = new List<SqlParameter>();
                string kiemtra = "select count (*) from the where MAKH=@tk ";
                _params1.Add(new SqlParameter("@tk", makh));
                string query = " ";
                if (con.Kiemtra(kiemtra, _params1) == 1)
                {
                    query = "update the set id_loaithe=@loaithe,diem=@diem where makh=@makh";

                }
                else
                {
                    query = "insert into the(trangthai,ngayhethan,id_loaithe,makh,ngaykichhoat,diem) values(1,@ngayhethan,@loaithe,@makh,@ngaykh,@diem)";
                }
                List<SqlParameter> _params = new List<SqlParameter>();
                _params.Add(new SqlParameter("@ngaykh", ngaykh));
                _params.Add(new SqlParameter("ngayhethan", ngayhethan));
                _params.Add(new SqlParameter("@loaithe", id_loaithe));
                _params.Add(new SqlParameter("@makh", makh));
                _params.Add(new SqlParameter("@diem", diem));
                con.excute(query, _params);



            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string msp = comboBox2.SelectedValue.ToString().Trim();
            string thanhtien = txttt.Text;
            string mnv = txtmnv.Text;
            string sl = txtsl.Text;
            string mkh = comboBox1.SelectedValue.ToString();

                var item = new Class1();
                item.masp = msp;
                item.soluong = sl;
                item.giatien = thanhtien;
                item.tongtien = txttt.Text;
          
            list.Add(item);
   
        }

     
        private void button6_Click_1(object sender, EventArgs e)
        {
         for(int i=0;i<list.Count;i++)
            {
              

                 if (this.panel1.Controls["pichd0"] == null)
                 {
                     PictureBox pic = new PictureBox();
                    pic.Location = new System.Drawing.Point(186, 151);
                    pic.Size = new System.Drawing.Size(100, 50);
                    pic.TabIndex = 4;
                    pic.BackColor = System.Drawing.SystemColors.ActiveCaption;
                     pic.Name = "pichd0";
    
                     pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                     pic.TabStop = false;
                     // pic.Text = files[i];

                     this.panel1.Controls.Add(pic);
                     getimg1(pic,list[i].masp);
                 }
                 else if (this.Controls["pichd" + i] == null)

                     TaopicBenDuoi((PictureBox)this.panel1.Controls["pichd" + (i-1)], "pichd" + i);
                 else if (i > list.Count)
                     this.Controls["pichd" + i].Dispose();

                 void TaopicBenDuoi(PictureBox TextBoxBenTren, string picName)
                 {

                     PictureBox tbx = new PictureBox();

                     tbx.Top = TextBoxBenTren.Bottom + 1;
                     tbx.Left = TextBoxBenTren.Left;
                     tbx.Width = TextBoxBenTren.Width;
                     tbx.Height = TextBoxBenTren.Height;
                     tbx.Name = picName;
                     getimg1(tbx,list[i].masp);
                     TextBoxBenTren.SizeMode = PictureBoxSizeMode.StretchImage;
                     this.panel1.Controls.Add(tbx);


                 }


         
                 if (this.panel1.Controls["texthda0"] == null)
                 {
                     TextBox tx = new TextBox();
                     tx.Location = new System.Drawing.Point(17, 43);
                     tx.Size = new System.Drawing.Size(63, 22);
                     tx.TabIndex = 1;
                     tx.Name = "texthda0";

                     this.panel1.Controls.Add(tx);
                     tx.Text = list[i].giatien;
                 }
                 else if (this.Controls["texthda" + i] == null)
                 {
                     TaoTextBoxBenDuoi((TextBox)this.panel1.Controls["texthda" + (i-1)], "texthda" + i);
                     this.panel1.Controls["texthda" + i].Text = list[i].giatien;
                 }

                 if (this.panel1.Controls["texthdb0"] == null)
                 {
                     TextBox tx = new TextBox();
                     tx.Location = new System.Drawing.Point(90, 43);
                     tx.Size = new System.Drawing.Size(63, 22);
                     tx.TabIndex = 2;
                     tx.Name = "texthdb0";

                     this.panel1.Controls.Add(tx);
                     tx.Text = list[i].masp;
                 }
                 else if (this.Controls["texthdb" + i] == null)
                 {
                     TaoTextBoxBenDuoi((TextBox)this.panel1.Controls["texthdb" + (i-1)], "texthdb" + i);
                     this.panel1.Controls["texthdb" + i].Text = list[i].masp;
                 }
                 if (this.panel1.Controls["texthdc0" ] == null)
                 {
                     TextBox tx = new TextBox();
                     tx.Location = new System.Drawing.Point(150, 43);
                     tx.Size = new System.Drawing.Size(63, 22);
                     tx.TabIndex = 3;
                     tx.Name = "texthdc0";

                     this.panel1.Controls.Add(tx);
                     tx.Text = list[i].soluong;
                 }
                 else
                 if (this.Controls["texthdc" + i] == null)
                 {
                     TaoTextBoxBenDuoi((TextBox)this.panel1.Controls["texthdc" + (i-1)], "texthdc" + i);
                     this.panel1.Controls["texthdc" + i ].Text = list[i].soluong;
                 }






                 void TaoTextBoxBenDuoi(TextBox TextBoxBenTren, String TextBoxName)
                 {

                     TextBox tbx = new TextBox();
                     tbx.Top = TextBoxBenTren.Bottom + 10;
                     tbx.Left = TextBoxBenTren.Left;
                     tbx.Width = TextBoxBenTren.Width;
                     tbx.Name = TextBoxName;
                     tbx.ScrollBars = TextBoxBenTren.ScrollBars;


                     this.panel1.Controls.Add(tbx);
                 }

                 
            }
            textBox2.Text = list.Count.ToString();
        }
        void Giamtien()
        {
            string con_str = "Data Source=LAPTOP-IS4TF3VU\\SQLEXPRESS;Initial Catalog=SONTUANHAU;User ID=SA;Password=123456;";
            SqlConnection conn = new SqlConnection(con_str);
         
            string s = comboBox1.SelectedValue.ToString().Trim();

            string Query = "SELECT diem FROM the WHERE MAKH='" + s + "'";


            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Hiển thị dữ liệu lên TEXT
            if (dt.Rows.Count > 0)
            {
                txtdiem.Text = dt.Rows[0]["diem"].ToString().Trim();
            }
            else
            {
                MessageBox.Show("Khoong");

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Giamtien();
        }
    }
}
