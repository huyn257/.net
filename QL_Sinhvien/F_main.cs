using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Sinhvien
{
    public partial class F_main : Form
    {
        public F_main()
        {
            InitializeComponent();
        }

        private void F_main_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult msg = MessageBox.Show("Ban co muon thoat hong?", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                Application.Exit();
        }

        private void khoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_khoa khoa = new F_khoa();
            khoa.Show();
            khoa.MdiParent = this;
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_lop lop = new F_lop();
            lop.Show();
            lop.MdiParent = this;
        }

        private void sinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_sinhvien sinhvien = new F_sinhvien();
            sinhvien.Show();
            sinhvien.MdiParent = this;
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_login login = new F_login();
            login.Show();
            login.MdiParent = this;
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_taikhoan taikhoan = new F_taikhoan();
            taikhoan.Show();
            taikhoan.MdiParent = this;
        }
    }
}
