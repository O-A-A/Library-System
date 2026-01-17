using LibrarySystem.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace LibrarySystem.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.txtPwd.PasswordChar = '*'; // 隐藏密码
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPwd.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入用户名和密码");
                return;
            }

            string sql = "SELECT COUNT(*) FROM sys_admin WHERE username=@u AND password=@p";
            MySqlParameter[] p = {
                new MySqlParameter("@u", username),
                new MySqlParameter("@p", password)
            };

            try
            {
                int count = Convert.ToInt32(MysqlHelper.ExecuteScalar(sql, p));
                if (count > 0)
                {
                    this.DialogResult = DialogResult.OK; // 返回 OK 给 Program.cs
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败：" + ex.Message);
            }
        }
    }
}