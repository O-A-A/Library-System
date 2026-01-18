namespace LibrarySystem.Forms
{
    partial class BorrowReturnForm
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
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBorrow = new System.Windows.Forms.TabPage();
            this.panelBorrow = new System.Windows.Forms.Panel();
            this.lblDueDateInfo = new System.Windows.Forms.Label();
            this.nudBorrowDays = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.txtB_StockID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtB_ReaderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBorrowIcon = new System.Windows.Forms.Label();
            this.tabReturn = new System.Windows.Forms.TabPage();
            this.panelReturn = new System.Windows.Forms.Panel();
            this.lblFinePreview = new System.Windows.Forms.Label();
            this.chkDamaged = new System.Windows.Forms.CheckBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.txtR_StockID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblReturnIcon = new System.Windows.Forms.Label();
            this.btnQueryBorrow = new System.Windows.Forms.Button();
            this.lblBorrowInfo = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabBorrow.SuspendLayout();
            this.panelBorrow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorrowDays)).BeginInit();
            this.tabReturn.SuspendLayout();
            this.panelReturn.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelTitle - 标题栏
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.panelTitle.Controls.Add(this.lblTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(600, 50);
            this.panelTitle.TabIndex = 0;

            // 
            // lblTitle - 标题文字
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(600, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📚 图书借阅与归还";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // tabControl1 - 选项卡控件
            // 
            this.tabControl1.Controls.Add(this.tabBorrow);
            this.tabControl1.Controls.Add(this.tabReturn);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(20, 5);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(600, 450);
            this.tabControl1.TabIndex = 1;

            // 
            // tabBorrow - 借阅选项卡
            // 
            this.tabBorrow.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.tabBorrow.Controls.Add(this.panelBorrow);
            this.tabBorrow.Location = new System.Drawing.Point(4, 32);
            this.tabBorrow.Name = "tabBorrow";
            this.tabBorrow.Padding = new System.Windows.Forms.Padding(20);
            this.tabBorrow.Size = new System.Drawing.Size(592, 414);
            this.tabBorrow.TabIndex = 0;
            this.tabBorrow.Text = "📖 图书借阅";

            // 
            // panelBorrow - 借阅内容面板
            // 
            this.panelBorrow.BackColor = System.Drawing.Color.White;
            this.panelBorrow.Controls.Add(this.lblDueDateInfo);
            this.panelBorrow.Controls.Add(this.lblBorrowIcon);
            this.panelBorrow.Controls.Add(this.label1);
            this.panelBorrow.Controls.Add(this.txtB_ReaderID);
            this.panelBorrow.Controls.Add(this.label2);
            this.panelBorrow.Controls.Add(this.txtB_StockID);
            this.panelBorrow.Controls.Add(this.label5);
            this.panelBorrow.Controls.Add(this.nudBorrowDays);
            this.panelBorrow.Controls.Add(this.btnBorrow);
            this.panelBorrow.Location = new System.Drawing.Point(70, 20);
            this.panelBorrow.Name = "panelBorrow";
            this.panelBorrow.Padding = new System.Windows.Forms.Padding(30);
            this.panelBorrow.Size = new System.Drawing.Size(450, 370);
            this.panelBorrow.TabIndex = 0;

            // 
            // lblBorrowIcon - 借阅图标
            // 
            this.lblBorrowIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 40F);
            this.lblBorrowIcon.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.lblBorrowIcon.Location = new System.Drawing.Point(175, 15);
            this.lblBorrowIcon.Name = "lblBorrowIcon";
            this.lblBorrowIcon.Size = new System.Drawing.Size(100, 70);
            this.lblBorrowIcon.TabIndex = 5;
            this.lblBorrowIcon.Text = "📖";
            this.lblBorrowIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // label1 - 读者ID标签
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label1.Location = new System.Drawing.Point(50, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "👤 读者ID：";

            // 
            // txtB_ReaderID
            // 
            this.txtB_ReaderID.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtB_ReaderID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_ReaderID.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.txtB_ReaderID.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtB_ReaderID.Location = new System.Drawing.Point(160, 97);
            this.txtB_ReaderID.Name = "txtB_ReaderID";
            this.txtB_ReaderID.Size = new System.Drawing.Size(220, 27);
            this.txtB_ReaderID.TabIndex = 1;

            // 
            // label2 - 库存ID标签
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label2.Location = new System.Drawing.Point(50, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "📦 库存ID：";

            // 
            // txtB_StockID
            // 
            this.txtB_StockID.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtB_StockID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_StockID.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.txtB_StockID.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtB_StockID.Location = new System.Drawing.Point(160, 142);
            this.txtB_StockID.Name = "txtB_StockID";
            this.txtB_StockID.Size = new System.Drawing.Size(220, 27);
            this.txtB_StockID.TabIndex = 3;

            // 
            // label5 - 借阅天数标签
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label5.Location = new System.Drawing.Point(50, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "📅 借阅天数：";

            // 
            // nudBorrowDays - 借阅天数选择
            // 
            this.nudBorrowDays.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.nudBorrowDays.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.nudBorrowDays.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.nudBorrowDays.Location = new System.Drawing.Point(160, 187);
            this.nudBorrowDays.Maximum = new decimal(new int[] { 90, 0, 0, 0 });
            this.nudBorrowDays.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudBorrowDays.Name = "nudBorrowDays";
            this.nudBorrowDays.Size = new System.Drawing.Size(80, 27);
            this.nudBorrowDays.TabIndex = 7;
            this.nudBorrowDays.Value = new decimal(new int[] { 30, 0, 0, 0 });
            this.nudBorrowDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudBorrowDays.ValueChanged += new System.EventHandler(this.nudBorrowDays_ValueChanged);

            // 
            // lblDueDateInfo - 应还日期提示
            // 
            this.lblDueDateInfo.AutoSize = true;
            this.lblDueDateInfo.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.lblDueDateInfo.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.lblDueDateInfo.Location = new System.Drawing.Point(250, 192);
            this.lblDueDateInfo.Name = "lblDueDateInfo";
            this.lblDueDateInfo.Size = new System.Drawing.Size(150, 20);
            this.lblDueDateInfo.TabIndex = 8;
            this.lblDueDateInfo.Text = "📆 应还：----年--月--日";

            // 
            // btnBorrow - 借阅按钮
            // 
            this.btnBorrow.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnBorrow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrow.FlatAppearance.BorderSize = 0;
            this.btnBorrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrow.Font = new System.Drawing.Font("Microsoft YaHei", 13F, System.Drawing.FontStyle.Bold);
            this.btnBorrow.ForeColor = System.Drawing.Color.White;
            this.btnBorrow.Location = new System.Drawing.Point(125, 270);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(200, 50);
            this.btnBorrow.TabIndex = 4;
            this.btnBorrow.Text = "📖 确认借阅";
            this.btnBorrow.UseVisualStyleBackColor = false;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);

            // 
            // tabReturn - 归还选项卡
            // 
            this.tabReturn.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.tabReturn.Controls.Add(this.panelReturn);
            this.tabReturn.Location = new System.Drawing.Point(4, 32);
            this.tabReturn.Name = "tabReturn";
            this.tabReturn.Padding = new System.Windows.Forms.Padding(20);
            this.tabReturn.Size = new System.Drawing.Size(592, 414);
            this.tabReturn.TabIndex = 1;
            this.tabReturn.Text = "📚 图书归还";

            // 
            // panelReturn - 归还内容面板
            // 
            this.panelReturn.BackColor = System.Drawing.Color.White;
            this.panelReturn.Controls.Add(this.lblReturnIcon);
            this.panelReturn.Controls.Add(this.label3);
            this.panelReturn.Controls.Add(this.txtR_StockID);
            this.panelReturn.Controls.Add(this.btnQueryBorrow);
            this.panelReturn.Controls.Add(this.lblBorrowInfo);
            this.panelReturn.Controls.Add(this.chkDamaged);
            this.panelReturn.Controls.Add(this.lblFinePreview);
            this.panelReturn.Controls.Add(this.btnReturn);
            this.panelReturn.Location = new System.Drawing.Point(70, 20);
            this.panelReturn.Name = "panelReturn";
            this.panelReturn.Padding = new System.Windows.Forms.Padding(30);
            this.panelReturn.Size = new System.Drawing.Size(450, 370);
            this.panelReturn.TabIndex = 0;

            // 
            // lblReturnIcon - 归还图标
            // 
            this.lblReturnIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 40F);
            this.lblReturnIcon.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.lblReturnIcon.Location = new System.Drawing.Point(175, 15);
            this.lblReturnIcon.Name = "lblReturnIcon";
            this.lblReturnIcon.Size = new System.Drawing.Size(100, 70);
            this.lblReturnIcon.TabIndex = 0;
            this.lblReturnIcon.Text = "📚";
            this.lblReturnIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // label3 - 库存ID标签
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label3.Location = new System.Drawing.Point(50, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "📦 库存ID：";

            // 
            // txtR_StockID
            // 
            this.txtR_StockID.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtR_StockID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtR_StockID.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.txtR_StockID.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtR_StockID.Location = new System.Drawing.Point(150, 97);
            this.txtR_StockID.Name = "txtR_StockID";
            this.txtR_StockID.Size = new System.Drawing.Size(150, 27);
            this.txtR_StockID.TabIndex = 2;

            // 
            // btnQueryBorrow - 查询借阅按钮
            // 
            this.btnQueryBorrow.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnQueryBorrow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQueryBorrow.FlatAppearance.BorderSize = 0;
            this.btnQueryBorrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryBorrow.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.btnQueryBorrow.ForeColor = System.Drawing.Color.White;
            this.btnQueryBorrow.Location = new System.Drawing.Point(310, 95);
            this.btnQueryBorrow.Name = "btnQueryBorrow";
            this.btnQueryBorrow.Size = new System.Drawing.Size(90, 30);
            this.btnQueryBorrow.TabIndex = 3;
            this.btnQueryBorrow.Text = "🔍 查询";
            this.btnQueryBorrow.UseVisualStyleBackColor = false;
            this.btnQueryBorrow.Click += new System.EventHandler(this.btnQueryBorrow_Click);

            // 
            // lblBorrowInfo - 借阅信息
            // 
            this.lblBorrowInfo.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.lblBorrowInfo.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblBorrowInfo.Location = new System.Drawing.Point(50, 140);
            this.lblBorrowInfo.Name = "lblBorrowInfo";
            this.lblBorrowInfo.Size = new System.Drawing.Size(350, 50);
            this.lblBorrowInfo.TabIndex = 4;
            this.lblBorrowInfo.Text = "请输入库存ID并点击查询";
            this.lblBorrowInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // chkDamaged - 损坏复选框
            // 
            this.chkDamaged.AutoSize = true;
            this.chkDamaged.Enabled = false;
            this.chkDamaged.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.chkDamaged.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.chkDamaged.Location = new System.Drawing.Point(55, 200);
            this.chkDamaged.Name = "chkDamaged";
            this.chkDamaged.Size = new System.Drawing.Size(168, 24);
            this.chkDamaged.TabIndex = 5;
            this.chkDamaged.Text = "⚠️ 图书损坏/丢失";
            this.chkDamaged.UseVisualStyleBackColor = true;
            this.chkDamaged.CheckedChanged += new System.EventHandler(this.chkDamaged_CheckedChanged);

            // 
            // lblFinePreview - 罚款预览
            // 
            this.lblFinePreview.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.lblFinePreview.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblFinePreview.Location = new System.Drawing.Point(50, 235);
            this.lblFinePreview.Name = "lblFinePreview";
            this.lblFinePreview.Size = new System.Drawing.Size(350, 40);
            this.lblFinePreview.TabIndex = 6;
            this.lblFinePreview.Text = "";
            this.lblFinePreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // btnReturn - 归还按钮
            // 
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.Enabled = false;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Microsoft YaHei", 13F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(125, 290);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(200, 50);
            this.btnReturn.TabIndex = 7;
            this.btnReturn.Text = "📚 确认归还";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);

            // 
            // BorrowReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BorrowReturnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图书借阅与归还";
            this.Load += new System.EventHandler(this.BorrowReturnForm_Load);
            this.panelTitle.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabBorrow.ResumeLayout(false);
            this.panelBorrow.ResumeLayout(false);
            this.panelBorrow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorrowDays)).EndInit();
            this.tabReturn.ResumeLayout(false);
            this.panelReturn.ResumeLayout(false);
            this.panelReturn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBorrow;
        private System.Windows.Forms.TabPage tabReturn;
        private System.Windows.Forms.Panel panelBorrow;
        private System.Windows.Forms.Panel panelReturn;
        private System.Windows.Forms.Label lblBorrowIcon;
        private System.Windows.Forms.Label lblReturnIcon;
        
        // 借书控件
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.TextBox txtB_StockID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtB_ReaderID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudBorrowDays;
        private System.Windows.Forms.Label lblDueDateInfo;
        
        // 还书控件
        private System.Windows.Forms.CheckBox chkDamaged;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.TextBox txtR_StockID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQueryBorrow;
        private System.Windows.Forms.Label lblBorrowInfo;
        private System.Windows.Forms.Label lblFinePreview;
    }
}