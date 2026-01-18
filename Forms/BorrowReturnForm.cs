using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using LibrarySystem.Helpers;

namespace LibrarySystem.Forms
{
    public partial class BorrowReturnForm : Form
    {
        // 罚款金额常量
        private const decimal OVERDUE_FINE_PER_DAY = 0.5m;  // 逾期罚款：每天0.5元
        private const decimal DAMAGE_FINE = 50.0m;          // 损坏罚款：50元

        // 库存状态常量
        private const int STATUS_AVAILABLE = 0;   // 可借
        private const int STATUS_BORROWED = 1;    // 已借出
        private const int STATUS_DAMAGED = 2;     // 损坏
        private const int STATUS_LOST = 3;        // 丢失

        // 当前查询到的借阅记录信息
        private int currentRecordId = 0;
        private int currentReaderId = 0;
        private DateTime currentDueDate;
        private int overdueDays = 0;
        private decimal overdueFine = 0;

        public BorrowReturnForm()
        {
            InitializeComponent();
        }

        private void BorrowReturnForm_Load(object sender, EventArgs e)
        {
            UpdateDueDateDisplay();
        }

        // 状态码转文字
        private string GetStatusText(int status)
        {
            switch (status)
            {
                case STATUS_AVAILABLE: return "可借";
                case STATUS_BORROWED: return "已借出";
                case STATUS_DAMAGED: return "损坏";
                case STATUS_LOST: return "丢失";
                default: return "未知";
            }
        }

        #region 借书功能

        // 借阅天数变化时更新应还日期显示
        private void nudBorrowDays_ValueChanged(object sender, EventArgs e)
        {
            UpdateDueDateDisplay();
        }

        // 更新应还日期显示
        private void UpdateDueDateDisplay()
        {
            DateTime dueDate = DateTime.Now.AddDays((int)nudBorrowDays.Value);
            lblDueDateInfo.Text = $"📆 应还：{dueDate:yyyy年MM月dd日}";
        }

        // 借书按钮
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtB_ReaderID.Text) ||
                string.IsNullOrWhiteSpace(txtB_StockID.Text))
            {
                MessageBox.Show("请输入读者ID和库存ID！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int readerId, stockId;
            if (!int.TryParse(txtB_ReaderID.Text, out readerId) ||
                !int.TryParse(txtB_StockID.Text, out stockId))
            {
                MessageBox.Show("ID必须是数字！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int borrowDays = (int)nudBorrowDays.Value;
            DateTime borrowDate = DateTime.Now;
            DateTime dueDate = borrowDate.AddDays(borrowDays);

            try
            {
                using (MySqlConnection conn = MysqlHelper.GetConnection())
                {
                    conn.Open();

                    // 检查读者是否存在
                    string checkReader = "SELECT name, total_unpaid_fines FROM readers WHERE reader_id = @ReaderID";
                    string readerName = "";
                    decimal unpaidFines = 0;
                    using (MySqlCommand cmd = new MySqlCommand(checkReader, conn))
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
                            readerName = reader.GetString("name");
                            unpaidFines = reader.GetDecimal("total_unpaid_fines");
                        }
                    }

                    // 检查是否有未缴罚款
                    if (unpaidFines > 0)
                    {
                        MessageBox.Show($"读者【{readerName}】有未缴罚款 {unpaidFines:F2} 元，请先缴清再借书！",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 检查库存是否可借
                    string checkStock = @"SELECT bs.status, b.book_name 
                                          FROM book_stock bs 
                                          JOIN books b ON bs.book_id = b.book_id 
                                          WHERE bs.stock_id = @StockID";
                    string bookTitle = "";
                    using (MySqlCommand cmd = new MySqlCommand(checkStock, conn))
                    {
                        cmd.Parameters.AddWithValue("@StockID", stockId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("库存不存在！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            int status = Convert.ToInt32(reader["status"]);
                            bookTitle = reader.GetString("book_name");
                            if (status != STATUS_AVAILABLE)
                            {
                                MessageBox.Show($"图书【{bookTitle}】当前状态为【{GetStatusText(status)}】，无法借阅！", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    // 插入借阅记录
                    string insertBorrow = @"INSERT INTO borrow_records 
                        (reader_id, stock_id, borrow_date, due_date) 
                        VALUES (@ReaderID, @StockID, @BorrowDate, @DueDate)";
                    using (MySqlCommand cmd = new MySqlCommand(insertBorrow, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReaderID", readerId);
                        cmd.Parameters.AddWithValue("@StockID", stockId);
                        cmd.Parameters.AddWithValue("@BorrowDate", borrowDate);
                        cmd.Parameters.AddWithValue("@DueDate", dueDate);
                        cmd.ExecuteNonQuery();
                    }

                    // 更新库存状态为已借出(1)
                    string updateStock = "UPDATE book_stock SET status = @Status WHERE stock_id = @StockID";
                    using (MySqlCommand cmd = new MySqlCommand(updateStock, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", STATUS_BORROWED);
                        cmd.Parameters.AddWithValue("@StockID", stockId);
                        cmd.ExecuteNonQuery();
                    }

                    // 更新读者借阅数量
                    string updateReader = "UPDATE readers SET borrowed_count = borrowed_count + 1 WHERE reader_id = @ReaderID";
                    using (MySqlCommand cmd = new MySqlCommand(updateReader, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReaderID", readerId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"✅ 借阅成功！\n\n" +
                        $"📖 图书：{bookTitle}\n" +
                        $"👤 读者：{readerName}\n" +
                        $"📅 借阅天数：{borrowDays}天\n" +
                        $"📆 应还日期：{dueDate:yyyy年MM月dd日}",
                        "借阅成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 清空输入
                    txtB_ReaderID.Clear();
                    txtB_StockID.Clear();
                    nudBorrowDays.Value = 30;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("借阅失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 还书功能

        // 查询借阅记录
        private void btnQueryBorrow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtR_StockID.Text))
            {
                MessageBox.Show("请输入库存ID！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int stockId;
            if (!int.TryParse(txtR_StockID.Text, out stockId))
            {
                MessageBox.Show("库存ID必须是数字！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = MysqlHelper.GetConnection())
                {
                    conn.Open();

                    // 查找未还的借阅记录
                    string query = @"SELECT br.record_id, br.reader_id, br.due_date, 
                                            r.name as reader_name, b.book_name as book_title
                                     FROM borrow_records br
                                     JOIN readers r ON br.reader_id = r.reader_id
                                     JOIN book_stock bs ON br.stock_id = bs.stock_id
                                     JOIN books b ON bs.book_id = b.book_id
                                     WHERE br.stock_id = @StockID AND br.return_date IS NULL";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StockID", stockId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                lblBorrowInfo.Text = "❌ 未找到该图书的借阅记录";
                                lblBorrowInfo.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
                                btnReturn.Enabled = false;
                                chkDamaged.Enabled = false;
                                lblFinePreview.Text = "";
                                return;
                            }

                            currentRecordId = reader.GetInt32("record_id");
                            currentReaderId = reader.GetInt32("reader_id");
                            currentDueDate = reader.GetDateTime("due_date");
                            string readerName = reader.GetString("reader_name");
                            string bookTitle = reader.GetString("book_title");

                            // 计算逾期天数
                            overdueDays = 0;
                            overdueFine = 0;
                            if (DateTime.Now > currentDueDate)
                            {
                                overdueDays = (DateTime.Now - currentDueDate).Days;
                                overdueFine = overdueDays * OVERDUE_FINE_PER_DAY;
                            }

                            // 显示借阅信息
                            lblBorrowInfo.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
                            lblBorrowInfo.Text = $"📖 {bookTitle}\n👤 借阅人：{readerName}  |  📅 应还日期：{currentDueDate:yyyy-MM-dd}";

                            // 启用归还按钮
                            btnReturn.Enabled = true;
                            chkDamaged.Enabled = true;
                            chkDamaged.Checked = false;

                            // 更新罚款预览
                            UpdateFinePreview();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 损坏复选框变化
        private void chkDamaged_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFinePreview();
        }

        // 更新罚款预览
        private void UpdateFinePreview()
        {
            decimal totalFine = overdueFine;
            if (chkDamaged.Checked)
            {
                totalFine += DAMAGE_FINE;
            }

            if (totalFine > 0)
            {
                string fineDetail = "";
                if (overdueFine > 0)
                    fineDetail += $"逾期{overdueDays}天：{overdueFine:F2}元";
                if (chkDamaged.Checked)
                {
                    if (fineDetail.Length > 0) fineDetail += " + ";
                    fineDetail += $"损坏：{DAMAGE_FINE:F2}元";
                }

                lblFinePreview.Text = $"⚠️ 预计罚款：{fineDetail}\n💰 总计：{totalFine:F2}元";
                lblFinePreview.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            }
            else
            {
                lblFinePreview.Text = "✅ 无罚款";
                lblFinePreview.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            }
        }

        // 还书按钮
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (currentRecordId == 0)
            {
                MessageBox.Show("请先查询借阅记录！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int stockId = int.Parse(txtR_StockID.Text);
            bool isDamaged = chkDamaged.Checked;
            DateTime returnDate = DateTime.Now;

            // 计算罚款
            decimal damageFine = isDamaged ? DAMAGE_FINE : 0;
            decimal totalFine = overdueFine + damageFine;

            try
            {
                using (MySqlConnection conn = MysqlHelper.GetConnection())
                {
                    conn.Open();
                    using (MySqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 更新借阅记录
                            string updateBorrow = @"UPDATE borrow_records 
                                                    SET return_date = @ReturnDate, 
                                                        fine_amount = @OverdueFine,
                                                        damage_fine = @DamageFine,
                                                        is_damaged = @IsDamaged
                                                    WHERE record_id = @RecordID";
                            using (MySqlCommand cmd = new MySqlCommand(updateBorrow, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                                cmd.Parameters.AddWithValue("@OverdueFine", overdueFine);
                                cmd.Parameters.AddWithValue("@DamageFine", damageFine);
                                cmd.Parameters.AddWithValue("@IsDamaged", isDamaged ? 1 : 0);
                                cmd.Parameters.AddWithValue("@RecordID", currentRecordId);
                                cmd.ExecuteNonQuery();
                            }

                            // 更新库存状态：损坏=2，可借=0
                            int newStatus = isDamaged ? STATUS_DAMAGED : STATUS_AVAILABLE;
                            string updateStock = "UPDATE book_stock SET status = @Status WHERE stock_id = @StockID";
                            using (MySqlCommand cmd = new MySqlCommand(updateStock, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@Status", newStatus);
                                cmd.Parameters.AddWithValue("@StockID", stockId);
                                cmd.ExecuteNonQuery();
                            }

                            // 更新读者借阅数量和罚款
                            string updateReader = @"UPDATE readers 
                                                    SET borrowed_count = borrowed_count - 1,
                                                        total_unpaid_fines = total_unpaid_fines + @TotalFine 
                                                    WHERE reader_id = @ReaderID";
                            using (MySqlCommand cmd = new MySqlCommand(updateReader, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@TotalFine", totalFine);
                                cmd.Parameters.AddWithValue("@ReaderID", currentReaderId);
                                cmd.ExecuteNonQuery();
                            }

                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                            throw;
                        }
                    }

                    // 显示归还结果
                    string message = "✅ 归还成功！";
                    if (totalFine > 0)
                    {
                        message += "\n\n⚠️ 产生罚款：";
                        if (overdueFine > 0)
                            message += $"\n  • 逾期{overdueDays}天：{overdueFine:F2}元（每天{OVERDUE_FINE_PER_DAY}元）";
                        if (damageFine > 0)
                            message += $"\n  • 图书损坏：{damageFine:F2}元";
                        message += $"\n\n💰 罚款总计：{totalFine:F2}元";
                        message += "\n\n请到【罚款缴费】处缴纳罚款。";
                    }

                    MessageBox.Show(message, "归还结果",
                        MessageBoxButtons.OK,
                        totalFine > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                    // 清空状态
                    txtR_StockID.Clear();
                    chkDamaged.Checked = false;
                    btnReturn.Enabled = false;
                    chkDamaged.Enabled = false;
                    lblBorrowInfo.Text = "请输入库存ID并点击查询";
                    lblFinePreview.Text = "";
                    currentRecordId = 0;
                    currentReaderId = 0;
                    overdueDays = 0;
                    overdueFine = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("归还失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}