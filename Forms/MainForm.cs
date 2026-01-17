using System;
using System.Windows.Forms;

namespace LibrarySystem.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "图书馆管理系统 - 主界面";
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true; // 设置为 MDI 容器
        }

        // 菜单：读者管理
        private void menuReader_Click(object sender, EventArgs e)
        {
            ReaderForm form = new ReaderForm();
            form.MdiParent = this;
            form.Show();
        }

        // 菜单：借阅归还
        private void menuBorrow_Click(object sender, EventArgs e)
        {
            BorrowReturnForm form = new BorrowReturnForm();
            form.MdiParent = this;
            form.Show();
        }

        // 菜单：退出
        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}