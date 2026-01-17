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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBorrow = new System.Windows.Forms.TabPage();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.txtB_StockID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtB_ReaderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabReturn = new System.Windows.Forms.TabPage();
            this.chkDamaged = new System.Windows.Forms.CheckBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.txtR_StockID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabBorrow.SuspendLayout();
            this.tabReturn.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBorrow);
            this.tabControl1.Controls.Add(this.tabReturn);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(600, 400);
            this.tabControl1.TabIndex = 0;
            // 
            // tabBorrow
            // 
            this.tabBorrow.Controls.Add(this.btnBorrow);
            this.tabBorrow.Controls.Add(this.txtB_StockID);
            this.tabBorrow.Controls.Add(this.label2);
            this.tabBorrow.Controls.Add(this.txtB_ReaderID);
            this.tabBorrow.Controls.Add(this.label1);
            this.tabBorrow.Location = new System.Drawing.Point(4, 22);
            this.tabBorrow.Name = "tabBorrow";
            this.tabBorrow.Padding = new System.Windows.Forms.Padding(3);
            this.tabBorrow.Size = new System.Drawing.Size(592, 374);
            this.tabBorrow.TabIndex = 0;
            this.tabBorrow.Text = "图书借阅";
            this.tabBorrow.UseVisualStyleBackColor = true;
            // 
            // btnBorrow
            // 
            this.btnBorrow.Location = new System.Drawing.Point(180, 150);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(120, 40);
            this.btnBorrow.TabIndex = 4;
            this.btnBorrow.Text = "办理借阅";
            this.btnBorrow.UseVisualStyleBackColor = true;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // txtB_StockID
            // 
            this.txtB_StockID.Location = new System.Drawing.Point(180, 90);
            this.txtB_StockID.Name = "txtB_StockID";
            this.txtB_StockID.Size = new System.Drawing.Size(200, 21);
            this.txtB_StockID.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "图书库存ID：";
            // 
            // txtB_ReaderID
            // 
            this.txtB_ReaderID.Location = new System.Drawing.Point(180, 50);
            this.txtB_ReaderID.Name = "txtB_ReaderID";
            this.txtB_ReaderID.Size = new System.Drawing.Size(200, 21);
            this.txtB_ReaderID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "读者ID：";
            // 
            // tabReturn
            // 
            this.tabReturn.Controls.Add(this.chkDamaged);
            this.tabReturn.Controls.Add(this.btnReturn);
            this.tabReturn.Controls.Add(this.txtR_StockID);
            this.tabReturn.Controls.Add(this.label3);
            this.tabReturn.Location = new System.Drawing.Point(4, 22);
            this.tabReturn.Name = "tabReturn";
            this.tabReturn.Padding = new System.Windows.Forms.Padding(3);
            this.tabReturn.Size = new System.Drawing.Size(592, 374);
            this.tabReturn.TabIndex = 1;
            this.tabReturn.Text = "图书归还";
            this.tabReturn.UseVisualStyleBackColor = true;
            // 
            // chkDamaged
            // 
            this.chkDamaged.AutoSize = true;
            this.chkDamaged.Location = new System.Drawing.Point(180, 90);
            this.chkDamaged.Name = "chkDamaged";
            this.chkDamaged.Size = new System.Drawing.Size(96, 16);
            this.chkDamaged.TabIndex = 7;
            this.chkDamaged.Text = "是否损坏/丢失";
            this.chkDamaged.UseVisualStyleBackColor = true;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(180, 130);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(120, 40);
            this.btnReturn.TabIndex = 6;
            this.btnReturn.Text = "办理归还";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // txtR_StockID
            // 
            this.txtR_StockID.Location = new System.Drawing.Point(180, 50);
            this.txtR_StockID.Name = "txtR_StockID";
            this.txtR_StockID.Size = new System.Drawing.Size(200, 21);
            this.txtR_StockID.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "图书库存ID：";
            // 
            // BorrowReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.tabControl1);
            this.Name = "BorrowReturnForm";
            this.Text = "借阅与归还";
            this.tabControl1.ResumeLayout(false);
            this.tabBorrow.ResumeLayout(false);
            this.tabBorrow.PerformLayout();
            this.tabReturn.ResumeLayout(false);
            this.tabReturn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBorrow;
        private System.Windows.Forms.TabPage tabReturn;
        
        // 借书页控件
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.TextBox txtB_StockID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtB_ReaderID;
        private System.Windows.Forms.Label label1;
        
        // 还书页控件
        private System.Windows.Forms.CheckBox chkDamaged;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.TextBox txtR_StockID;
        private System.Windows.Forms.Label label3;
    }
}