namespace LibrarySystem.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuReader = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPublish = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBook = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStock = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReader,
            this.menuPublish,
            this.menuBook,
            this.menuBorrow,
            this.menuStock,
            this.menuExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuReader
            // 
            this.menuReader.Name = "menuReader";
            this.menuReader.Size = new System.Drawing.Size(68, 21);
            this.menuReader.Text = "读者管理";
            this.menuReader.Click += new System.EventHandler(this.menuReader_Click);
            // 
            // menuPublish
            // 
            this.menuPublish.Name = "menuPublish";
            this.menuPublish.Size = new System.Drawing.Size(68, 21);
            this.menuPublish.Text = "出版社管理";
            this.menuPublish.Click += new System.EventHandler(this.menuPublish_Click);
            // 
            // menuBook
            // 
            this.menuBook.Name = "menuBook";
            this.menuBook.Size = new System.Drawing.Size(68, 21);
            this.menuBook.Text = "图书管理";
            this.menuBook.Click += new System.EventHandler(this.menuBook_Click);
            // 
            // menuBorrow
// 
            this.menuBorrow.Name = "menuBorrow";
            this.menuBorrow.Size = new System.Drawing.Size(68, 21);
            this.menuBorrow.Text = "借阅归还";
            this.menuBorrow.Click += new System.EventHandler(this.menuBorrow_Click);



            this.menuStock.Name = "menuStock";
            this.menuStock.Size = new System.Drawing.Size(68, 21);
            this.menuStock.Text = "库存管理";
            this.menuStock.Click += new System.EventHandler(this.menuStock_Click);
            // 新增
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(44, 21);
            this.menuExit.Text = "退出";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "图书馆管理系统";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuReader;
        private System.Windows.Forms.ToolStripMenuItem menuPublish;
        private System.Windows.Forms.ToolStripMenuItem menuBook;
        private System.Windows.Forms.ToolStripMenuItem menuBorrow;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuStock;

    }
}