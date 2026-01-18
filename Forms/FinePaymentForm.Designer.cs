namespace LibrarySystem.Forms
{
    partial class FinePaymentForm
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
            this.panelQuery = new System.Windows.Forms.Panel();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFineType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPay = new System.Windows.Forms.Button();
            this.txtPayAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUnpaidAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReaderName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReaderId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvFineRecords = new System.Windows.Forms.DataGridView();
            this.dgvPayment = new System.Windows.Forms.DataGridView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lblFineRecords = new System.Windows.Forms.Label();
            this.lblPayHistory = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            this.panelQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFineRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelTitle - 标题栏
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.panelTitle.Controls.Add(this.lblTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(950, 50);
            this.panelTitle.TabIndex = 0;

            // 
            // lblTitle - 标题文字
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(950, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💰 读者罚款缴费管理";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // panelQuery - 查询操作区
            // 
            this.panelQuery.BackColor = System.Drawing.Color.White;
            this.panelQuery.Controls.Add(this.btnQuery);
            this.panelQuery.Controls.Add(this.txtOperator);
            this.panelQuery.Controls.Add(this.label7);
            this.panelQuery.Controls.Add(this.txtFineType);
            this.panelQuery.Controls.Add(this.label6);
            this.panelQuery.Controls.Add(this.btnPay);
            this.panelQuery.Controls.Add(this.txtPayAmount);
            this.panelQuery.Controls.Add(this.label5);
            this.panelQuery.Controls.Add(this.txtUnpaidAmount);
            this.panelQuery.Controls.Add(this.label4);
            this.panelQuery.Controls.Add(this.txtReaderName);
            this.panelQuery.Controls.Add(this.label3);
            this.panelQuery.Controls.Add(this.txtReaderId);
            this.panelQuery.Controls.Add(this.label2);
            this.panelQuery.Controls.Add(this.label1);
            this.panelQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQuery.Location = new System.Drawing.Point(0, 50);
            this.panelQuery.Name = "panelQuery";
            this.panelQuery.Padding = new System.Windows.Forms.Padding(15);
            this.panelQuery.Size = new System.Drawing.Size(950, 110);
            this.panelQuery.TabIndex = 1;

            // 
            // label2 - 读者ID标签
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label2.Location = new System.Drawing.Point(15, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "👤 读者ID：";

            // 
            // txtReaderId
            // 
            this.txtReaderId.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtReaderId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReaderId.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.txtReaderId.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtReaderId.Location = new System.Drawing.Point(100, 20);
            this.txtReaderId.Name = "txtReaderId";
            this.txtReaderId.Size = new System.Drawing.Size(80, 25);
            this.txtReaderId.TabIndex = 2;

            // 
            // btnQuery - 查询按钮
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.FlatAppearance.BorderSize = 0;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(190, 18);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 28);
            this.btnQuery.TabIndex = 14;
            this.btnQuery.Text = "🔍 查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);

            // 
            // label3 - 读者姓名标签
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label3.Location = new System.Drawing.Point(280, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "姓名：";

            // 
            // txtReaderName
            // 
            this.txtReaderName.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtReaderName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReaderName.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.txtReaderName.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtReaderName.Location = new System.Drawing.Point(335, 20);
            this.txtReaderName.Name = "txtReaderName";
            this.txtReaderName.ReadOnly = true;
            this.txtReaderName.Size = new System.Drawing.Size(100, 25);
            this.txtReaderName.TabIndex = 4;

            // 
            // label4 - 未缴罚款标签
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.label4.Location = new System.Drawing.Point(450, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "⚠️ 未缴罚款总额：";

            // 
            // txtUnpaidAmount
            // 
            this.txtUnpaidAmount.BackColor = System.Drawing.Color.FromArgb(253, 237, 236);
            this.txtUnpaidAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUnpaidAmount.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.txtUnpaidAmount.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.txtUnpaidAmount.Location = new System.Drawing.Point(575, 20);
            this.txtUnpaidAmount.Name = "txtUnpaidAmount";
            this.txtUnpaidAmount.ReadOnly = true;
            this.txtUnpaidAmount.Size = new System.Drawing.Size(100, 25);
            this.txtUnpaidAmount.TabIndex = 6;
            this.txtUnpaidAmount.Text = "0.00 元";
            this.txtUnpaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // 
            // label5 - 缴费金额标签
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label5.Location = new System.Drawing.Point(15, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "💵 缴费金额：";

            // 
            // txtPayAmount
            // 
            this.txtPayAmount.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtPayAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPayAmount.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.txtPayAmount.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtPayAmount.Location = new System.Drawing.Point(110, 63);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.ReadOnly = true;
            this.txtPayAmount.Size = new System.Drawing.Size(100, 25);
            this.txtPayAmount.TabIndex = 8;

            // 
            // label6 - 罚款类型标签
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label6.Location = new System.Drawing.Point(225, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "📋 罚款类型：";

            // 
            // txtFineType - 罚款类型（只读，自动判断）
            // 
            this.txtFineType.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtFineType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFineType.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.txtFineType.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtFineType.Location = new System.Drawing.Point(320, 63);
            this.txtFineType.Name = "txtFineType";
            this.txtFineType.ReadOnly = true;
            this.txtFineType.Size = new System.Drawing.Size(120, 25);
            this.txtFineType.TabIndex = 11;
            this.txtFineType.Text = "请选择记录";
            this.txtFineType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // 
            // label7 - 操作员标签
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.label7.Location = new System.Drawing.Point(455, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "👤 操作员：";

            // 
            // txtOperator
            // 
            this.txtOperator.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtOperator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperator.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.txtOperator.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtOperator.Location = new System.Drawing.Point(540, 63);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(100, 25);
            this.txtOperator.TabIndex = 13;
            this.txtOperator.Text = "管理员";

            // 
            // btnPay - 确认缴费按钮
            // 
            this.btnPay.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPay.Enabled = false;
            this.btnPay.FlatAppearance.BorderSize = 0;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Bold);
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(700, 18);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(220, 70);
            this.btnPay.TabIndex = 9;
            this.btnPay.Text = "💳 确认缴费";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);

            // 
            // label1 - 占位标签
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;

            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 160);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;

            // 
            // splitContainer.Panel1 - 未缴罚款记录
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvFineRecords);
            this.splitContainer.Panel1.Controls.Add(this.lblFineRecords);
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.White;

            // 
            // splitContainer.Panel2 - 缴费历史记录
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgvPayment);
            this.splitContainer.Panel2.Controls.Add(this.lblPayHistory);
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer.Size = new System.Drawing.Size(950, 440);
            this.splitContainer.SplitterDistance = 220;
            this.splitContainer.TabIndex = 2;

            // 
            // lblFineRecords - 未缴罚款标题
            // 
            this.lblFineRecords.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblFineRecords.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFineRecords.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.lblFineRecords.ForeColor = System.Drawing.Color.White;
            this.lblFineRecords.Location = new System.Drawing.Point(0, 0);
            this.lblFineRecords.Name = "lblFineRecords";
            this.lblFineRecords.Size = new System.Drawing.Size(950, 28);
            this.lblFineRecords.TabIndex = 0;
            this.lblFineRecords.Text = "⚠️ 未缴罚款记录（点击选择要缴费的记录，金额和类型自动填充）";
            this.lblFineRecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFineRecords.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);

            // 
            // dgvFineRecords - 未缴罚款表格
            // 
            this.dgvFineRecords.AllowUserToAddRows = false;
            this.dgvFineRecords.AllowUserToResizeRows = false;
            this.dgvFineRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFineRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvFineRecords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFineRecords.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvFineRecords.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvFineRecords.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.FromArgb(231, 76, 60),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold),
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvFineRecords.ColumnHeadersHeight = 35;
            this.dgvFineRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFineRecords.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                Font = new System.Drawing.Font("Microsoft YaHei", 9F),
                SelectionBackColor = System.Drawing.Color.FromArgb(231, 76, 60),
                SelectionForeColor = System.Drawing.Color.White
            };
            this.dgvFineRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFineRecords.EnableHeadersVisualStyles = false;
            this.dgvFineRecords.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.dgvFineRecords.Location = new System.Drawing.Point(0, 28);
            this.dgvFineRecords.Name = "dgvFineRecords";
            this.dgvFineRecords.ReadOnly = true;
            this.dgvFineRecords.RowHeadersVisible = false;
            this.dgvFineRecords.RowTemplate.Height = 30;
            this.dgvFineRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFineRecords.Size = new System.Drawing.Size(950, 192);
            this.dgvFineRecords.TabIndex = 1;
            this.dgvFineRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFineRecords_CellClick);

            // 
            // lblPayHistory - 缴费历史标题
            // 
            this.lblPayHistory.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.lblPayHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPayHistory.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold);
            this.lblPayHistory.ForeColor = System.Drawing.Color.White;
            this.lblPayHistory.Location = new System.Drawing.Point(0, 0);
            this.lblPayHistory.Name = "lblPayHistory";
            this.lblPayHistory.Size = new System.Drawing.Size(950, 28);
            this.lblPayHistory.TabIndex = 0;
            this.lblPayHistory.Text = "✅ 缴费历史记录";
            this.lblPayHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPayHistory.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);

            // 
            // dgvPayment - 缴费记录表格
            // 
            this.dgvPayment.AllowUserToAddRows = false;
            this.dgvPayment.AllowUserToResizeRows = false;
            this.dgvPayment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayment.BackgroundColor = System.Drawing.Color.White;
            this.dgvPayment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPayment.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPayment.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPayment.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.FromArgb(46, 204, 113),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold),
                Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            };
            this.dgvPayment.ColumnHeadersHeight = 35;
            this.dgvPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPayment.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                Font = new System.Drawing.Font("Microsoft YaHei", 9F),
                SelectionBackColor = System.Drawing.Color.FromArgb(46, 204, 113),
                SelectionForeColor = System.Drawing.Color.White
            };
            this.dgvPayment.AlternatingRowsDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor = System.Drawing.Color.FromArgb(245, 247, 250)
            };
            this.dgvPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayment.EnableHeadersVisualStyles = false;
            this.dgvPayment.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.dgvPayment.Location = new System.Drawing.Point(0, 28);
            this.dgvPayment.Name = "dgvPayment";
            this.dgvPayment.ReadOnly = true;
            this.dgvPayment.RowHeadersVisible = false;
            this.dgvPayment.RowTemplate.Height = 30;
            this.dgvPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayment.Size = new System.Drawing.Size(950, 188);
            this.dgvPayment.TabIndex = 1;

            // 
            // FinePaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelQuery);
            this.Controls.Add(this.panelTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FinePaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "读者罚款缴费管理";
            this.Load += new System.EventHandler(this.FinePaymentForm_Load);
            this.panelTitle.ResumeLayout(false);
            this.panelQuery.ResumeLayout(false);
            this.panelQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFineRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelQuery;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtOperator;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFineType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.TextBox txtPayAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUnpaidAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReaderName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReaderId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvFineRecords;
        private System.Windows.Forms.DataGridView dgvPayment;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblFineRecords;
        private System.Windows.Forms.Label lblPayHistory;
    }
}