using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using LibrarySystem.Helpers;

namespace LibrarySystem.Forms
{
    public partial class FinePaymentForm : Form
    {
        private int currentReaderId = 0;
        private decimal currentUnpaidAmount = 0;

        public FinePaymentForm()
        {
            InitializeComponent();
        }

        private void FinePaymentForm_Load(object sender, EventArgs e)
        {
            cmbFineType.SelectedIndex = 0;
        }

        // 查询读者罚款信息
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReaderId.Text))
            {
                MessageBox.Show("请输入读者ID！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int readerId;
            if (!int.TryParse(txtReaderId.Text, out readerId))
            {
                MessageBox.Show("读者ID必须是数字！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = MysqlHelper.GetConnection())
                {
                    conn.Open();

                    // 查询读者信息
                    string queryReader = "SELECT name, total_unpaid_fines FROM readers WHERE reader_id = @ReaderID";
                    using (MySqlCommand cmd = new MySqlCommand(queryReader, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReaderID", readerId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("读者不存在！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            txtReaderName.Text = reader.GetString("name");
                            currentUnpaidAmount = reader.GetDecimal("total_unpaid_fines");
                            txtUnpaidAmount.Text = $"{currentUnpaidAmount:F2} 元";
                            currentReaderId = readerId;
                        }
                    }

                    // 查询未缴罚款的借阅记录
                    LoadFineRecords(conn, readerId);

                    // 查询缴费历史
                    LoadPaymentHistory(conn, readerId);

                    // 启用缴费按钮
                    btnPay.Enabled = currentUnpaidAmount > 0;

                    if (currentUnpaidAmount <= 0)
                    {
                        txtPayAmount.Text = "";
                        MessageBox.Show("该读者没有未缴罚款！", "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 加载未缴罚款记录
        private void LoadFineRecords(MySqlConnection conn, int readerId)
        {
            string query = @"SELECT 
                                br.record_id AS '记录ID',
                                b.book_name AS '书名',
                                br.borrow_date AS '借阅日期',
                                br.due_date AS '应还日期',
                                br.return_date AS '归还日期',
                                br.fine_amount AS '逾期罚款',
                                IFNULL(br.damage_fine, 0) AS '损坏罚款',
                                (br.fine_amount + IFNULL(br.damage_fine, 0)) AS '罚款合计',
                                CASE 
                                    WHEN br.fine_amount > 0 AND IFNULL(br.damage_fine, 0) > 0 THEN '逾期+损坏'
                                    WHEN br.fine_amount > 0 THEN '逾期'
                                    WHEN IFNULL(br.damage_fine, 0) > 0 THEN '损坏'
                                    ELSE '无'
                                END AS '罚款类型'
                             FROM borrow_records br
                             JOIN book_stock bs ON br.stock_id = bs.stock_id
                             JOIN books b ON bs.book_id = b.book_id
                             WHERE br.reader_id = @ReaderID 
                               AND br.return_date IS NOT NULL
                               AND (br.fine_amount > 0 OR IFNULL(br.damage_fine, 0) > 0)
                               AND br.is_fine_paid = 0
                             ORDER BY br.return_date DESC";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ReaderID", readerId);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvFineRecords.DataSource = dt;
                }
            }
        }

        // 加载缴费历史
        private void LoadPaymentHistory(MySqlConnection conn, int readerId)
        {
            string query = @"SELECT 
                                pay_id AS '缴费ID',
                                pay_amount AS '缴费金额',
                                IFNULL(fine_type, '逾期罚款') AS '罚款类型',
                                pay_time AS '缴费时间',
                                pay_desc AS '缴费说明',
                                operator AS '操作员'
                             FROM fine_payment
                             WHERE reader_id = @ReaderID
                             ORDER BY pay_time DESC";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ReaderID", readerId);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPayment.DataSource = dt;
                }
            }
        }

        // 点击未缴罚款记录，自动填充金额
        private void dgvFineRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvFineRecords.Rows[e.RowIndex];
                decimal fineTotal = Convert.ToDecimal(row.Cells["罚款合计"].Value);
                object fineTypeObj = row.Cells["罚款类型"].Value;
                string fineType = fineTypeObj != null ? fineTypeObj.ToString() : "逾期";

                txtPayAmount.Text = fineTotal.ToString("F2");

                // 设置罚款类型
                switch (fineType)
                {
                    case "逾期":
                        cmbFineType.SelectedIndex = 0;
                        break;
                    case "损坏":
                        cmbFineType.SelectedIndex = 1;
                        break;
                    case "逾期+损坏":
                        cmbFineType.SelectedIndex = 2;
                        break;
                    default:
                        cmbFineType.SelectedIndex = 0;
                        break;
                }
            }
        }

        // 确认缴费
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (currentReaderId == 0)
            {
                MessageBox.Show("请先查询读者信息！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPayAmount.Text))
            {
                MessageBox.Show("请输入缴费金额！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal payAmount;
            if (!decimal.TryParse(txtPayAmount.Text, out payAmount) || payAmount <= 0)
            {
                MessageBox.Show("请输入有效的缴费金额！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (payAmount > currentUnpaidAmount)
            {
                MessageBox.Show($"缴费金额不能超过未缴罚款总额 {currentUnpaidAmount:F2} 元！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fineType = cmbFineType.SelectedItem != null ? cmbFineType.SelectedItem.ToString() : "逾期罚款";
            string operatorName = string.IsNullOrWhiteSpace(txtOperator.Text) ? "管理员" : txtOperator.Text;

            // 确认缴费
            DialogResult result = MessageBox.Show(
                $"确认缴费信息：\n\n" +
                $"👤 读者：{txtReaderName.Text}\n" +
                $"💰 缴费金额：{payAmount:F2} 元\n" +
                $"📋 罚款类型：{fineType}\n" +
                $"👤 操作员：{operatorName}\n\n" +
                $"确认缴费吗？",
                "确认缴费", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                using (MySqlConnection conn = MysqlHelper.GetConnection())
                {
                    conn.Open();
                    using (MySqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 获取选中的记录ID
                            int? recordId = null;
                            if (dgvFineRecords.SelectedRows.Count > 0 &&
                                dgvFineRecords.SelectedRows[0].Cells["记录ID"].Value != null)
                            {
                                recordId = Convert.ToInt32(dgvFineRecords.SelectedRows[0].Cells["记录ID"].Value);
                            }

                            // 插入缴费记录
                            string insertPayment = @"INSERT INTO fine_payment 
                                (reader_id, record_id, pay_amount, fine_type, pay_desc, operator) 
                                VALUES (@ReaderID, @RecordID, @PayAmount, @FineType, @PayDesc, @Operator)";

                            using (MySqlCommand cmd = new MySqlCommand(insertPayment, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ReaderID", currentReaderId);
                                cmd.Parameters.AddWithValue("@RecordID", recordId.HasValue ? (object)recordId.Value : DBNull.Value);
                                cmd.Parameters.AddWithValue("@PayAmount", payAmount);
                                cmd.Parameters.AddWithValue("@FineType", fineType);
                                cmd.Parameters.AddWithValue("@PayDesc", $"{fineType}缴费");
                                cmd.Parameters.AddWithValue("@Operator", operatorName);
                                cmd.ExecuteNonQuery();
                            }

                            // 更新读者未缴罚款
                            string updateReader = @"UPDATE readers 
                                                    SET total_unpaid_fines = total_unpaid_fines - @PayAmount 
                                                    WHERE reader_id = @ReaderID";
                            using (MySqlCommand cmd = new MySqlCommand(updateReader, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@PayAmount", payAmount);
                                cmd.Parameters.AddWithValue("@ReaderID", currentReaderId);
                                cmd.ExecuteNonQuery();
                            }

                            // 如果选中了具体记录，标记该记录罚款已缴
                            if (recordId.HasValue)
                            {
                                // 检查是否全部缴清
                                string checkFine = @"SELECT fine_amount + IFNULL(damage_fine, 0) as total 
                                                     FROM borrow_records WHERE record_id = @RecordID";
                                decimal recordFine = 0;
                                using (MySqlCommand cmd = new MySqlCommand(checkFine, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@RecordID", recordId.Value);
                                    object result2 = cmd.ExecuteScalar();
                                    if (result2 != null && result2 != DBNull.Value)
                                    {
                                        recordFine = Convert.ToDecimal(result2);
                                    }
                                }

                                if (payAmount >= recordFine)
                                {
                                    string updateRecord = "UPDATE borrow_records SET is_fine_paid = 1 WHERE record_id = @RecordID";
                                    using (MySqlCommand cmd = new MySqlCommand(updateRecord, conn, trans))
                                    {
                                        cmd.Parameters.AddWithValue("@RecordID", recordId.Value);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            trans.Commit();

                            MessageBox.Show($"✅ 缴费成功！\n\n缴费金额：{payAmount:F2} 元", "成功",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 刷新数据
                            btnQuery_Click(sender, e);
                            txtPayAmount.Clear();
                        }
                        catch
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("缴费失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}