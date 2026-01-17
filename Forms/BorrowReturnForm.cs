using LibrarySystem.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibrarySystem.Forms
{
    public partial class BorrowReturnForm : Form
    {
        public BorrowReturnForm()
        {
            InitializeComponent();
        }

        // ================= 借书逻辑 =================
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            string rIdStr = txtB_ReaderID.Text.Trim();
            string sIdStr = txtB_StockID.Text.Trim();

            if (string.IsNullOrEmpty(rIdStr) || string.IsNullOrEmpty(sIdStr))
            {
                MessageBox.Show("请输入读者ID和图书库存ID");
                return;
            }

            int readerId = int.Parse(rIdStr);
            int stockId = int.Parse(sIdStr);

            using (MySqlConnection conn = MysqlHelper.GetConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;

                try
                {
                    // 1. 检查图书状态
                    cmd.CommandText = "SELECT status FROM book_stock WHERE stock_id = @sid";
                    cmd.Parameters.AddWithValue("@sid", stockId);
                    object statusObj = cmd.ExecuteScalar();
                    
                    if (statusObj == null) throw new Exception("图书ID不存在");
                    if (Convert.ToInt32(statusObj) != 0) throw new Exception("该书已被借出或处于异常状态");

                    // 2. 插入借阅记录
                    cmd.CommandText = @"INSERT INTO borrow_records (reader_id, stock_id, borrow_date, due_date) 
                                        VALUES (@rid, @sid, NOW(), DATE_ADD(NOW(), INTERVAL 30 DAY))";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@rid", readerId);
                    cmd.Parameters.AddWithValue("@sid", stockId);
                    cmd.ExecuteNonQuery();

                    // 3. 更新库存状态 -> 1 (借出)
                    cmd.CommandText = "UPDATE book_stock SET status = 1 WHERE stock_id = @sid";
                    cmd.ExecuteNonQuery();

                    // 4. 更新读者已借数量
                    cmd.CommandText = "UPDATE readers SET borrowed_count = borrowed_count + 1 WHERE reader_id = @rid";
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                    MessageBox.Show("借阅成功！");
                    txtB_StockID.Clear(); // 清空方便下一本
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("借阅失败：" + ex.Message);
                }
            }
        }

        // ================= 还书逻辑 =================
        private void btnReturn_Click(object sender, EventArgs e)
        {
            string sIdStr = txtR_StockID.Text.Trim();
            if (string.IsNullOrEmpty(sIdStr)) return;

            int stockId = int.Parse(sIdStr);
            bool isDamaged = chkDamaged.Checked;

            using (MySqlConnection conn = MysqlHelper.GetConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;

                try
                {
                    // 1. 查找借阅记录
                    cmd.CommandText = @"SELECT record_id, reader_id, due_date FROM borrow_records 
                                        WHERE stock_id = @sid AND return_date IS NULL";
                    cmd.Parameters.AddWithValue("@sid", stockId);
                    
                    DataTable dt = new DataTable();
                    new MySqlDataAdapter(cmd).Fill(dt);

                    if (dt.Rows.Count == 0) throw new Exception("该书没有在借记录");

                    int recordId = Convert.ToInt32(dt.Rows[0]["record_id"]);
                    int readerId = Convert.ToInt32(dt.Rows[0]["reader_id"]);
                    DateTime dueDate = Convert.ToDateTime(dt.Rows[0]["due_date"]);

                    // 2. 计算罚款
                    decimal fine = 0;
                    if (DateTime.Now > dueDate)
                    {
                        int days = (DateTime.Now - dueDate).Days;
                        fine += days * 0.5m; // 每天0.5元
                    }
                    if (isDamaged) fine += 20.0m; // 损坏罚款

                    // 3. 更新借阅表
                    cmd.CommandText = @"UPDATE borrow_records SET return_date = NOW(), fine_amount = @fine 
                                        WHERE record_id = @recId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@fine", fine);
                    cmd.Parameters.AddWithValue("@recId", recordId);
                    cmd.ExecuteNonQuery();

                    // 4. 更新库存状态 (0:正常, 2:损坏)
                    cmd.CommandText = "UPDATE book_stock SET status = @st WHERE stock_id = @sid";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@st", isDamaged ? 2 : 0);
                    cmd.Parameters.AddWithValue("@sid", stockId);
                    cmd.ExecuteNonQuery();

                    // 5. 更新读者表
                    cmd.CommandText = @"UPDATE readers SET borrowed_count = borrowed_count - 1, 
                                        total_unpaid_fines = total_unpaid_fines + @fine 
                                        WHERE reader_id = @rid";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@fine", fine);
                    cmd.Parameters.AddWithValue("@rid", readerId);
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                    
                    string msg = "归还成功！";
                    if (fine > 0) msg += $"\n产生罚款：{fine} 元";
                    MessageBox.Show(msg);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("归还失败：" + ex.Message);
                }
            }
        }
    }
}