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
            this.panelQuery = new System.Windows.Forms.Panel();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPayDesc = new System.Windows.Forms.TextBox();
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
            this.dgvPayment = new System.Windows.Forms.DataGridView();
            this.panelQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).BeginInit();
            this.SuspendLayout();
            // 
            // panelQuery
            // 
            this.panelQuery.Controls.Add(this.btnQuery);
            this.panelQuery.Controls.Add(this.txtOperator);
            this.panelQuery.Controls.Add(this.label7);
            this.panelQuery.Controls.Add(this.txtPayDesc);
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
            this.panelQuery.Location = new System.Drawing.Point(0, 0);
            this.panelQuery.Name = "panelQuery";
            this.panelQuery.Size = new System.Drawing.Size(900, 120);
            this.panelQuery.TabIndex = 0;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(260, 25);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 14;
            this.btnQuery.Text = "查询读者";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtOperator
            // 
            this.txtOperator.Location = new System.Drawing.Point(600, 65);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(100, 21);
            this.txtOperator.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(553, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "操作员：";
            // 
            // txtPayDesc
            // 
            this.txtPayDesc.Location = new System.Drawing.Point(390, 65);
            this.txtPayDesc.Name = "txtPayDesc";
            this.txtPayDesc.Size = new System.Drawing.Size(150, 21);
            this.txtPayDesc.TabIndex = 11;
            this.txtPayDesc.Text = "逾期罚款+图书损坏罚款";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(343, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "说明：";
            // 
            // btnPay
            // 
            this.btnPay.Enabled = false;
            this.btnPay.Location = new System.Drawing.Point(720, 65);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(75, 23);
            this.btnPay.TabIndex = 9;
            this.btnPay.Text = "确认缴费";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // txtPayAmount
// 
            this.txtPayAmount.Location = new System.Drawing.Point(260, 65);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.Size = new System.Drawing.Size(70, 21);
            this.txtPayAmount.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "缴费：";
            // 
            // txtUnpaidAmount
            // 
            this.txtUnpaidAmount.Location = new System.Drawing.Point(120, 65);
            this.txtUnpaidAmount.Name = "txtUnpaidAmount";
            this.txtUnpaidAmount.ReadOnly = true;
            this.txtUnpaidAmount.Size = new System.Drawing.Size(80, 21);
            this.txtUnpaidAmount.TabIndex = 6;
            this.txtUnpaidAmount.Text = "0.00 元";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "当前未缴罚款总额：";
            // 
            // txtReaderName
            // 
            this.txtReaderName.Location = new System.Drawing.Point(120, 25);
            this.txtReaderName.Name = "txtReaderName";
            this.txtReaderName.ReadOnly = true;
            this.txtReaderName.Size = new System.Drawing.Size(130, 21);
            this.txtReaderName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "读者姓名：";
            // 
            // txtReaderId
            // 
            this.txtReaderId.Location = new System.Drawing.Point(53, 25);
            this.txtReaderId.Name = "txtReaderId";
            this.txtReaderId.Size = new System.Drawing.Size(50, 21);
            this.txtReaderId.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "读者ID：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // dgvPayment
            // 
            this.dgvPayment.AllowUserToAddRows = false;
            this.dgvPayment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayment.Location = new System.Drawing.Point(0, 120);
            this.dgvPayment.Name = "dgvPayment";
            this.dgvPayment.ReadOnly = true;
            this.dgvPayment.RowTemplate.Height = 23;
            this.dgvPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayment.Size = new System.Drawing.Size(900, 330);
            this.dgvPayment.TabIndex = 1;
            // 
            // FinePaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Controls.Add(this.dgvPayment);
            this.Controls.Add(this.panelQuery);
            this.Name = "FinePaymentForm";
            this.Text = "读者罚款缴费管理";
            this.Load += new System.EventHandler(this.FinePaymentForm_Load);
            this.panelQuery.ResumeLayout(false);
            this.panelQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panelQuery;
        private System.Windows.Forms.DataGridView dgvPayment;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtOperator;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPayDesc;
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
    }
}