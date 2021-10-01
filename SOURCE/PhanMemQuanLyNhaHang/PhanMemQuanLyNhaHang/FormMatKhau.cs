using PhanMemQuanLyNhaHang.XuLy;
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
    public partial class FormDoiMatKhau : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public FormDoiMatKhau(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
        }

        void ChangeAccount(Account acc)
        {
            txtUserName.Text = LoginAccount.TenNV;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void UpdateAccountInfo()
        {
            string password = txtPassWord.Text;
            string newpass = txtNewPass.Text;
            string reenterPass = txtNewPass2.Text;
            string userName = txtUserName.Text;
            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới!");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    if (updateAccount != null)
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu!");
                }
            }
        }

        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtPassWord.TextLength < 6)
            {
                MessageBox.Show("Mật khẩu không đủ 6 kí tự !");
                return;
            }

            if (txtNewPass.TextLength < 6)
            {
                MessageBox.Show("Mật khẩu mới không đủ 6 kí tự !");
                return;
            }

            UpdateAccountInfo();
            this.Close();
        }
    }

    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
