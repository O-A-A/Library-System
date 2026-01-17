using LibrarySystem.Forms;
using System;
using System.Windows.Forms;

namespace LibrarySystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 启动登录窗体
            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                // 登录成功后启动主窗体
                Application.Run(new MainForm());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}