using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyNhaHang
{
    public partial class FormKhoa : Form
    {
        private string password;
        public FormKhoa()
        {
            InitializeComponent();
        }

        public FormKhoa(string user, string name, string pass) : this()
        {
            lblName.Text = name;
            password = pass;
        }

        //khi nhap vao mot ki tu thi goi ham kiem tra
        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            check();
        }

        private void check()
        {
            if (txtMatKhau.Text == password)
            {
                //nhap dung mat khaus
                this.Close();
            }
            else
            {
                txtMatKhau.Focus();
                pbErr.Visible = true;
            }
        }

        private void txtMatKhau_Leave(object sender, EventArgs e)
        {
            txtMatKhau.Focus();
        }

        private void Block_Leave(object sender, EventArgs e)
        {
            txtMatKhau.Focus();
        }
    }
}
