using LibrarySystem.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibrarySystem.Forms
{
    public partial class FinePaymentForm : Form
    {
        public FinePaymentForm()
        {
            InitializeComponent();
        }

        #region 窗体加载事件
        private void FinePaymentForm_Load(object sender, EventArgs e)
        {
            txtOperator.Text = "管理员";
            LoadPaymentList(); // 加载所有缴费记录
        }
        #endregion

        #region 1. 根据读者ID查询未缴罚款  ✅【修复点1：reader_name → name】
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string readerIdStr = txtReaderId.Text.Trim();
            if (string.IsNullOrWhiteSpace(readerIdStr) || !int.TryParse(readerIdStr, out int readerId))
            {
                MessageBox.Show("请输入正确的纯数字读者ID！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReaderId.Focus();
                return;
            }

            // ✅ 关键修改：reader_name 改为 name 适配你的读者表字段
            string sql = "SELECT reader_id, name, total_unpaid_fines FROM readers WHERE reader_id = @rid";
            MySqlParameter param = new MySqlParameter("@rid", readerId);
            DataTable dt = MysqlHelper.ExecuteDataTable(sql, param);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该读者ID不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearQueryInfo();
                return;
            }

            // 回填读者信息和未缴罚款
            DataRow row = dt.Rows[0];
            txtReaderName.Text = row["name"].ToString(); // ✅ 同步修改：row["name"]
            decimal unpaidFine = Convert.ToDecimal(row["total_unpaid_fines"]);
            txtUnpaidAmount.Text = unpaidFine.ToString("0.00") + " 元";

            // 缴费金额默认填充未缴总额
            txtPayAmount.Text = unpaidFine.ToString("0.00");

            if (unpaidFine <= 0)
            {
                MessageBox.Show("该读者暂无未缴纳罚款！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnPay.Enabled = false;
            }
            else
            {
                btnPay.Enabled = true;
            }
        }
        #endregion

        #region 2. 办理罚款缴费核心逻辑（重中之重，无修改）
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReaderId.Text) || !int.TryParse(txtReaderId.Text.Trim(), out int readerId))
            {
                MessageBox.Show("请先查询读者信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPayAmount.Text) || !decimal.TryParse(txtPayAmount.Text.Trim(), out decimal payAmount) || payAmount <= 0)
            {
                MessageBox.Show("请输入正确的缴费金额（大于0的数字）！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPayAmount.Focus();
                return;
            }

            decimal unpaidFine = Convert.ToDecimal(txtUnpaidAmount.Text.Replace(" 元", ""));
            if (payAmount > unpaidFine)
            {
                MessageBox.Show($"缴费金额不能大于未缴罚款总额【{unpaidFine}元】！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 开启数据库事务：缴费记录+更新罚款 要么都成功，要么都失败
            using (MySqlConnection conn = MysqlHelper.GetConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;

                try
                {
                    // ========== 步骤1：插入缴费记录到缴费表（留存历史，永不删除） ==========
                    cmd.CommandText = @"INSERT INTO fine_payment (reader_id, pay_amount, pay_desc, operator)
                                        VALUES (@rid, @amount, @desc, @operator)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@rid", readerId);
                    cmd.Parameters.AddWithValue("@amount", payAmount);
                    cmd.Parameters.AddWithValue("@desc", txtPayDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@operator", txtOperator.Text.Trim());
                    cmd.ExecuteNonQuery();

                    // ========== 步骤2：更新读者表的未缴罚款金额（扣除本次缴费金额） ==========
                    cmd.CommandText = @"UPDATE readers 
                                        SET total_unpaid_fines = total_unpaid_fines - @amount 
                                        WHERE reader_id = @rid";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@amount", payAmount);
                    cmd.Parameters.AddWithValue("@rid", readerId);
                    cmd.ExecuteNonQuery();

                    // 提交事务
                    trans.Commit();
                    MessageBox.Show($"罚款缴费成功！本次缴费：{payAmount} 元", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // 刷新页面
                    ClearQueryInfo();
                    LoadPaymentList();
                }
                catch (Exception ex)
                {
                    // 回滚事务
                    trans.Rollback();
                    MessageBox.Show("缴费失败：" + ex.Message, "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 3. 加载所有缴费记录列表 ✅【修复点2：r.reader_name → r.name】
        private void LoadPaymentList()
        {
            // ✅ 关键修改：r.reader_name 改为 r.name 适配你的读者表字段
            string sql = @"SELECT fp.pay_id, fp.reader_id, r.name, fp.pay_amount, fp.pay_time, fp.pay_desc, fp.operator
                           FROM fine_payment fp
                           LEFT JOIN readers r ON fp.reader_id = r.reader_id
                           ORDER BY fp.pay_time DESC";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            dgvPayment.DataSource = dt;

            // 设置表格表头 ✅ 同步修改：读者姓名 无需改，只是显示文本
            dgvPayment.Columns["pay_id"].HeaderText = "缴费编号";
            dgvPayment.Columns["reader_id"].HeaderText = "读者ID";
            dgvPayment.Columns["name"].HeaderText = "读者姓名"; // ✅ 对应修改后的字段名name
            dgvPayment.Columns["pay_amount"].HeaderText = "缴费金额(元)";
            dgvPayment.Columns["pay_time"].HeaderText = "缴费时间";
            dgvPayment.Columns["pay_desc"].HeaderText = "缴费说明";
            dgvPayment.Columns["operator"].HeaderText = "操作员";
        }
        #endregion

        #region 辅助方法：清空查询信息
        private void ClearQueryInfo()
        {
            txtReaderId.Clear();
            txtReaderName.Clear();
            txtUnpaidAmount.Text = "0.00 元";
            txtPayAmount.Clear();
            txtPayDesc.Text = "逾期罚款+图书损坏罚款";
            btnPay.Enabled = false;
        }
        #endregion
    }
}