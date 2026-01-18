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
            this.menuFine = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();

            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // 
            // menuStrip1 - 美化菜单栏
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuReader,
                this.menuPublish,
                this.menuBook,
                this.menuBorrow,
                this.menuStock,
                this.menuFine,
                this.menuExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.menuStrip1.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);

            // 
            // menuReader
            // 
            this.menuReader.Text = "👤 读者管理";
            this.menuReader.ForeColor = System.Drawing.Color.White;
            this.menuReader.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.menuReader.Click += new System.EventHandler(this.menuReader_Click);

            // 
            // menuPublish
            // 
            this.menuPublish.Text = "🏢 出版社管理";
            this.menuPublish.ForeColor = System.Drawing.Color.White;
            this.menuPublish.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.menuPublish.Click += new System.EventHandler(this.menuPublish_Click);

            // 
            // menuBook
            // 
            this.menuBook.Text = "📚 图书管理";
            this.menuBook.ForeColor = System.Drawing.Color.White;
            this.menuBook.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.menuBook.Click += new System.EventHandler(this.menuBook_Click);

            // 
            // menuBorrow
            // 
            this.menuBorrow.Text = "🔄 借阅归还";
            this.menuBorrow.ForeColor = System.Drawing.Color.White;
            this.menuBorrow.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.menuBorrow.Click += new System.EventHandler(this.menuBorrow_Click);

            // 
            // menuStock
            // 
            this.menuStock.Text = "📦 库存管理";
            this.menuStock.ForeColor = System.Drawing.Color.White;
            this.menuStock.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.menuStock.Click += new System.EventHandler(this.menuStock_Click);

            // 
            // menuFine
            // 
            this.menuFine.Text = "💰 罚款缴费";
            this.menuFine.ForeColor = System.Drawing.Color.White;
            this.menuFine.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.menuFine.Click += new System.EventHandler(this.menuFine_Click);

            // 
            // menuExit
            // 
            this.menuExit.Text = "❌ 退出";
            this.menuExit.ForeColor = System.Drawing.Color.White;
            this.menuExit.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);

            // 
            // statusStrip1 - 美化状态栏
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1000, 22);
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.statusStrip1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Text = "✓ 系统就绪";
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft YaHei", 10F);

            // 
            // panelMain - 主内容背景
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 30);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1000, 548);
            this.panelMain.TabIndex = 2;
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(245, 246, 248);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "图书馆管理系统";
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuReader;
        private System.Windows.Forms.ToolStripMenuItem menuPublish;
        private System.Windows.Forms.ToolStripMenuItem menuBook;
        private System.Windows.Forms.ToolStripMenuItem menuBorrow;
        private System.Windows.Forms.ToolStripMenuItem menuStock;
        private System.Windows.Forms.ToolStripMenuItem menuFine;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panelMain;
    }
}