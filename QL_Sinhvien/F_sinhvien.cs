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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QL_Sinhvien
{
    public partial class F_sinhvien : Form
    {
        string strcon = @"Data Source=DESKTOP-G8TI83K\MSSQLSERVER02;
Initial Catalog=QL_Sinhvien;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        public void clear_form()
        {
            txtmasv.Clear();
            txthoten.Clear();
            txtsdt.Clear();
            txtemail.Clear();
            txtdiachi.Clear();
        }
        public F_sinhvien()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        public void hienthisv()
        {
            try
            {
                conn = new SqlConnection(strcon);
                conn.Open();
                string str_lop = "select * from t_sinhvien";
                da = new SqlDataAdapter(str_lop, conn);
                dt = new DataTable();
                da.Fill(dt);
                DGVsv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            conn.Close();
        }
        public void loadmalop()
        {
            conn = new SqlConnection(strcon);
            conn.Open();
            string str_load = "select malop from t_lop";
            cmd = new SqlCommand(str_load, conn);
            IDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                cbbmalop.Items.Add(rd["malop"].ToString());
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtmasv.Text == "")
            {
                MessageBox.Show("Chua nhap ma lop!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmasv.Focus();
            }
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string gioiTinh = "";
                    if (rdnam.Checked)
                    {
                        gioiTinh = "Nam";
                    }
                    else if (rdnu.Checked)
                    {
                        gioiTinh = "Nữ";
                    }
                    
                    string str_insert = "insert into t_sinhvien values('" + txtmasv.Text + "',N'" + txthoten.Text + "',@gioiTinh,'" + txtemail.Text + "','" + txtsdt.Text + "',N'" + txtdiachi.Text + "','" + cbbmalop.Text + "')";
                    cmd = new SqlCommand(str_insert, conn);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Them thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_form();
                    txtmasv.Focus();
                    hienthisv();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        private void F_sinhvien_Load(object sender, EventArgs e)
        {
            hienthisv();
            loadmalop();
        }

        private void DGVsv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtmasv.Text = DGVsv.Rows[dong].Cells[0].Value.ToString();
            txthoten.Text = DGVsv.Rows[dong].Cells[1].Value.ToString();
            txtemail.Text = DGVsv.Rows[dong].Cells[3].Value.ToString();
            txtsdt.Text = DGVsv.Rows[dong].Cells[4].Value.ToString();
            txtdiachi.Text = DGVsv.Rows[dong].Cells[5].Value.ToString();
            cbbmalop.Text = DGVsv.Rows[dong].Cells[6].Value.ToString();
        }

        private void DGVsv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVsv.CurrentRow.Cells["gioitinh"].Value.ToString() == "Nam")
            {
                rdnam.Checked = true;
            }
            else if (DGVsv.CurrentRow.Cells["gioitinh"].Value.ToString() == "Nữ")
            {
                rdnu.Checked = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtmasv.Text == "")
            {
                MessageBox.Show("Chua nhap ma lop!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmasv.Focus();
            }
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string gioiTinh = "";
                    if (rdnam.Checked)
                    {
                        gioiTinh = "Nam";
                    }
                    else if (rdnu.Checked)
                    {
                        gioiTinh = "Nữ";
                    }

                    string str_update = "update t_sinhvien set hoten = N'" + txthoten.Text + "', gioitinh = @gioiTinh , email = '" + txtemail.Text + "', sdt = '" + txtsdt.Text + "', diachi = N'" + txtdiachi.Text + "', malop = '" + cbbmalop.Text  + "'";
                    cmd = new SqlCommand(str_update, conn);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sua thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_form();
                    txtmasv.Focus();
                    hienthisv();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi ket noi!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtmasv.Text == "")
            {
                MessageBox.Show("Chua nhap ma lop!", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmasv.Focus();
            }
            else
            {
                try
                {
                    conn = new SqlConnection(strcon);
                    conn.Open();
                    string str_del = "delete from t_sinhvien where masv ='" + txtmasv.Text + "'";
                    cmd = new SqlCommand(str_del, conn);
                    cmd.ExecuteNonQuery();
                    DialogResult msg = MessageBox.Show("Ban co muon xoa khong?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msg == DialogResult.OK)
                    {
                        clear_form();
                    }
                    txtmasv.Focus();
                    hienthisv();
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
                    string sql_search = "select * from t_sinhvien where masv='" + txttimkiem.Text + "'";
                    da = new SqlDataAdapter(sql_search, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    DGVsv.DataSource = dt;
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
