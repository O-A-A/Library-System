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
        private int? currentRecordId = null;  // 当前选中的记录ID
        private string currentFineType = "";  // 当前罚款类型

        public FinePaymentForm()
        {
            InitializeComponent();
        }

        private void FinePaymentForm_Load(object sender, EventArgs e)
        {
            ClearSelection();
        }

        // 清空选择状态
        private void ClearSelection()
        {
            currentRecordId = null;
            currentFineType = "";
            txtPayAmount.Text = "";
            txtFineType.Text = "请选择记录";
            btnPay.Enabled = false;
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
                            txtUnpaidAmount.Text = currentUnpaidAmount.ToString("F2") + " 元";
                            currentReaderId = readerId;
                        }
                    }

                    // 查询未缴罚款的借阅记录
                    LoadFineRecords(conn, readerId);

                    // 查询缴费历史
                    LoadPaymentHistory(conn, readerId);

                    // 清空选择状态
                    ClearSelection();

                    if (currentUnpaidAmount <= 0)
                    {
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
                                DATEDIFF(br.return_date, br.due_date) AS '逾期天数',
                                br.fine_amount AS '逾期罚款',
                                IFNULL(br.damage_fine, 0) AS '损坏罚款',
                                (br.fine_amount + IFNULL(br.damage_fine, 0)) AS '罚款合计',
                                CASE 
                                    WHEN br.fine_amount > 0 AND IFNULL(br.damage_fine, 0) > 0 THEN '逾期+损坏'
                                    WHEN br.fine_amount > 0 THEN '逾期罚款'
                                    WHEN IFNULL(br.damage_fine, 0) > 0 THEN '损坏罚款'
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

                    // 隐藏逾期天数为负数或空的情况
                    if (dgvFineRecords.Columns["逾期天数"] != null)
                    {
                        dgvFineRecords.Columns["逾期天数"].DefaultCellStyle.Format = "0 天";
                    }
                }
            }
        }

        // 加载缴费历史
        private void LoadPaymentHistory(MySqlConnection conn, int readerId)
        {
            string query = @"SELECT 
                                pay_id AS '缴费ID',
                                pay_amount AS '缴费金额',
                                fine_type AS '罚款类型',
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

        // 点击未缴罚款记录，自动填充金额和类型
        private void dgvFineRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvFineRecords.Rows.Count)
            {
                DataGridViewRow row = dgvFineRecords.Rows[e.RowIndex];
                
                // 获取记录ID
                currentRecordId = Convert.ToInt32(row.Cells["记录ID"].Value);
                
                // 获取罚款金额
                decimal fineTotal = Convert.ToDecimal(row.Cells["罚款合计"].Value);
                txtPayAmount.Text = fineTotal.ToString("F2");
                
                // 获取罚款类型（自动判断，不可修改）
                currentFineType = row.Cells["罚款类型"].Value?.ToString() ?? "逾期罚款";
                txtFineType.Text = currentFineType;

                // 根据罚款类型设置颜色
                switch (currentFineType)
                {
                    case "逾期罚款":
                        txtFineType.BackColor = System.Drawing.Color.FromArgb(255, 243, 205);
                        txtFineType.ForeColor = System.Drawing.Color.FromArgb(133, 100, 4);
                        break;
                    case "损坏罚款":
                        txtFineType.BackColor = System.Drawing.Color.FromArgb(248, 215, 218);
                        txtFineType.ForeColor = System.Drawing.Color.FromArgb(114, 28, 36);
                        break;
                    case "逾期+损坏":
                        txtFineType.BackColor = System.Drawing.Color.FromArgb(253, 237, 236);
                        txtFineType.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
                        break;
                    default:
                        txtFineType.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
                        txtFineType.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
                        break;
                }

                // 启用缴费按钮
                btnPay.Enabled = true;
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

            if (currentRecordId == null)
            {
                MessageBox.Show("请在上方表格中选择要缴费的罚款记录！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPayAmount.Text))
            {
                MessageBox.Show("请选择要缴费的记录！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal payAmount;
            if (!decimal.TryParse(txtPayAmount.Text, out payAmount) || payAmount <= 0)
            {
                MessageBox.Show("缴费金额无效！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string operatorName = string.IsNullOrWhiteSpace(txtOperator.Text) ? "管理员" : txtOperator.Text;

            // 确认缴费
            DialogResult result = MessageBox.Show(
                "确认缴费信息：\n\n" +
                "👤 读者：" + txtReaderName.Text + "\n" +
                "💰 缴费金额：" + payAmount.ToString("F2") + " 元\n" +
                "📋 罚款类型：" + currentFineType + "\n" +
                "👤 操作员：" + operatorName + "\n\n" +
                "确认缴费吗？",
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
                            // 插入缴费记录
                            string insertPayment = @"INSERT INTO fine_payment 
                                (reader_id, record_id, pay_amount, fine_type, pay_desc, operator) 
                                VALUES (@ReaderID, @RecordID, @PayAmount, @FineType, @PayDesc, @Operator)";

                            using (MySqlCommand cmd = new MySqlCommand(insertPayment, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ReaderID", currentReaderId);
                                cmd.Parameters.AddWithValue("@RecordID", currentRecordId.Value);
                                cmd.Parameters.AddWithValue("@PayAmount", payAmount);
                                cmd.Parameters.AddWithValue("@FineType", currentFineType);
                                cmd.Parameters.AddWithValue("@PayDesc", currentFineType + " - 已缴清");
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

                            // 标记该记录罚款已缴
                            string updateRecord = "UPDATE borrow_records SET is_fine_paid = 1 WHERE record_id = @RecordID";
                            using (MySqlCommand cmd = new MySqlCommand(updateRecord, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@RecordID", currentRecordId.Value);
                                cmd.ExecuteNonQuery();
                            }

                            trans.Commit();

                            MessageBox.Show(
                                "✅ 缴费成功！\n\n" +
                                "缴费金额：" + payAmount.ToString("F2") + " 元\n" +
                                "罚款类型：" + currentFineType,
                                "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 刷新数据
                            btnQuery_Click(sender, e);
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