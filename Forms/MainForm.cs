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

        // 打开出版社管理窗体 按钮事件
        private void menuPublish_Click(object sender, EventArgs e)
        {
            // 实例化出版社窗体，弹窗打开，最常用的Winform窗体打开方式
            PublishForm publishForm = new PublishForm();
            publishForm.ShowDialog();
        }

        private void menuStock_Click(object sender, EventArgs e)
        {
            StockForm stockForm = new StockForm();
            stockForm.ShowDialog();
        }

        //图书管理菜单点击事件
        private void menuBook_Click(object sender, EventArgs e)
        {
            BookForm bookForm = new BookForm();
            bookForm.ShowDialog();
        }

        // 菜单：退出
        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}