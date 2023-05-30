using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace QL_Sinhvien
{
    public partial class F_khoa : Form

    {
        string strcon = @"Data Source=DESKTOP-G8TI83K\MSSQLSERVER02;
Initial Catalog=QL_Sinhvien;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public F_khoa()
        {
            InitializeComponent();
        }
        public void clear_form()
        {
            txtmakhoa.Clear();
            txttenkhoa.Clear();
            txtsdt.Clear();
            txtemail.Clear();
            txtdiachi.Clear();
        }

        // Xóa:
        private void button3_Click(object sender, EventArgs e)
        {
            if (txtmakhoa.Text == "")
            {
                MessageBox.Show("Chua nhap ma khoa!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmakhoa.Focus();
            }
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string str_del = "delete from t_khoa where makhoa ='"+txtmakhoa.Text+"'";
                    cmd = new SqlCommand(str_del, conn);
                    cmd.ExecuteNonQuery();
                    DialogResult msg = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.OK)
                    {
                        clear_form();                   
                    }
                    txtmakhoa.Focus();
                    hienthikhoa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa khoa này!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void hienthikhoa()
        {
            try
            {
                conn = new SqlConnection(strcon);
                conn.Open();
                string str_khoa = "select * from t_khoa";
                da = new SqlDataAdapter(str_khoa, conn);
                dt = new DataTable();
                da.Fill(dt);
                DGVkhoa.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            conn.Close();
        }

        private void F_khoa_Load(object sender, EventArgs e)
        {
            hienthikhoa();
        }

        //Thêm,ngoại lệ:
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtmakhoa.Text == "")
            {
                MessageBox.Show("Chua nhap ma khoa!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmakhoa.Focus();
            }
 
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    String check_makhoa = "select count(*) from t_khoa where makhoa='" + txtmakhoa.Text + "'";
                    cmd = new SqlCommand(check_makhoa, conn);
                    int count = (int)cmd.ExecuteScalar();
                    if (count== 0)
                    { 
                        string str_insert = "insert into t_khoa values('" + txtmakhoa.Text + "',N'" + txttenkhoa.Text + "','" + txtemail.Text + "','" + txtsdt.Text + "',N'" + txtdiachi.Text + "')";
                        cmd = new SqlCommand(str_insert, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Them thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear_form();
                        txtmakhoa.Focus();
                        hienthikhoa();
                    }
                    else
                    {
                        MessageBox.Show("Ma khoa da ton tai!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        hienthikhoa();
                    }
                }
                catch 
                {
                    MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            conn.Close();

        }

        //sửa:
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtmakhoa.Text == "")
            {
                MessageBox.Show("Chua nhap ma khoa!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmakhoa.Focus();
            }
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string str_update = "update t_khoa set tenkhoa=N'"+txttenkhoa.Text+"',sdt='"+txtsdt.Text+"',email='"+txtemail.Text+"',diachi=N'"+txtdiachi.Text+"' where makhoa ='" + txtmakhoa.Text + "'";
                    cmd = new SqlCommand(str_update, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sua thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear_form();
                        txtmakhoa.Focus();
                        hienthikhoa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        //tìm kiếm:
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttimkiem.Text == "")
                {
                    MessageBox.Show("Chưa nhập thông tin","Thông báo",
                        MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql_search = "select * from t_khoa where makhoa='"+txttimkiem.Text+"'";
                    da = new SqlDataAdapter(sql_search, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    DGVkhoa.DataSource = dt;
                }
            }
            catch
            {
                MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            conn.Close();
        }

        private void DGVkhoa_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtmakhoa.Text = DGVkhoa.Rows[dong].Cells[0].Value.ToString();
            txttenkhoa.Text = DGVkhoa.Rows[dong].Cells[1].Value.ToString();
            txtsdt.Text = DGVkhoa.Rows[dong].Cells[2].Value.ToString();
            txtemail.Text = DGVkhoa.Rows[dong].Cells[3].Value.ToString();
            txtdiachi.Text = DGVkhoa.Rows[dong].Cells[4].Value.ToString();
        }
        //Đóng: 
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
