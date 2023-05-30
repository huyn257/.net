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

namespace QL_Sinhvien
{
    public partial class F_lop : Form
    {
        string strcon = @"Data Source=DESKTOP-G8TI83K\MSSQLSERVER02;
Initial Catalog=QL_Sinhvien;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
 
        public void clear_form()
        {
            txtmalop.Clear();
            txttenlop.Clear();
            txtsdt.Clear();
            txtemail.Clear();
            txtdiachi.Clear();
        }
        public F_lop()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbmakhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtmalop.Text == "")
            {
                MessageBox.Show("Chua nhap ma lop!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmalop.Focus();
            }
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string str_insert = "insert into t_lop values('" + txtmalop.Text + "',N'" + txttenlop.Text + "','" + txtemail.Text + "','" + txtsdt.Text + "',N'" + txtdiachi.Text + "','"+cbbmakhoa.Text+"')";
                    cmd = new SqlCommand(str_insert, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Them thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_form();
                    txtmalop.Focus();
                    hienthilop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        public void hienthilop()
        {
            try
            {
                conn = new SqlConnection(strcon);
                conn.Open();
                string str_lop = "select * from t_lop";
                da = new SqlDataAdapter(str_lop, conn);
                dt = new DataTable();
                da.Fill(dt);
                DGVlop.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            conn.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public void loadmakhoa()
        {
            conn = new SqlConnection(strcon);
            conn.Open();
            string str_load = "select makhoa from t_khoa";
            cmd = new SqlCommand(str_load, conn);
            IDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                cbbmakhoa.Items.Add(rd["makhoa"].ToString());
            }
            conn.Close();
        }

        private void F_lop_Load(object sender, EventArgs e)
        {
            loadmakhoa();
            hienthilop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtmalop.Text == "")
            {
                MessageBox.Show("Chua nhap ma lop!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmalop.Focus();
            }
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string str_update = "update t_lop set tenlop = N'" + txttenlop.Text + "',email = '" + txtemail.Text + "',sdt = " + txtsdt.Text + ",diachi =  N'" + txtdiachi.Text + "',makhoa = '" + cbbmakhoa.Text + "' where malop ='" + txtmalop.Text + "'";
                    cmd = new SqlCommand(str_update, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sua thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_form();
                    txtmalop.Focus();
                    hienthilop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        private void DGVLop_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtmalop.Text = DGVlop.Rows[dong].Cells[0].Value.ToString();
            txttenlop.Text = DGVlop.Rows[dong].Cells[1].Value.ToString();
            txtemail.Text = DGVlop.Rows[dong].Cells[2].Value.ToString();
            txtsdt.Text = DGVlop.Rows[dong].Cells[3].Value.ToString();
            txtdiachi.Text = DGVlop.Rows[dong].Cells[4].Value.ToString();
            cbbmakhoa.Text = DGVlop.Rows[dong].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtmalop.Text == "")
            {
                MessageBox.Show("Chua nhap ma lop!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmalop.Focus();
            }
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string str_del = "delete from t_lop where malop ='" + txtmalop.Text + "'";
                    cmd = new SqlCommand(str_del, conn);
                    cmd.ExecuteNonQuery();
                    DialogResult msg = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.OK)
                    {
                        clear_form(); 
                    }
                    txtmalop.Focus();
                    hienthilop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa lớp này!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttimkiem.Text == "")
                {
                    MessageBox.Show("Chưa nhập thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql_search = "select * from t_lop where malop='" + txttimkiem.Text + "'";
                    da = new SqlDataAdapter(sql_search, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    DGVlop.DataSource = dt;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
