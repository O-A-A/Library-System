using LibrarySystem.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibrarySystem.Forms
{
    public partial class StockForm : Form
    {
        private int currentStockId = -1;

        public StockForm()
        {
            InitializeComponent();
        }

        #region 窗体加载事件
        private void StockForm_Load(object sender, EventArgs e)
        {
            LoadBookList();
            InitLocationComboBox();
            LoadStockList();
        }
        #endregion

        #region 初始化位置下拉框
        private void InitLocationComboBox()
        {
            if (cmbLocation.Items.Count > 0)
            {
                cmbLocation.SelectedIndex = 0;
            }
            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }
        }
        #endregion

        #region 加载图书下拉框（显示库存信息）
        private void LoadBookList()
        {
            // 查询图书信息，包含库存数量和已入库数量
            string sql = @"SELECT b.book_id, 
                                  CONCAT(b.book_name, ' [库存:', b.book_stock, ' 已入库:', 
                                         IFNULL((SELECT COUNT(*) FROM book_stock WHERE book_id = b.book_id), 0), ']') AS display_name,
                                  b.book_name
                           FROM books b 
                           ORDER BY b.book_name ASC";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            cmbBook.DataSource = dt;
            cmbBook.DisplayMember = "display_name";
            cmbBook.ValueMember = "book_id";
            
            if (dt.Rows.Count > 0)
            {
                cmbBook.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("当前没有图书数据，请先添加图书！", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 加载馆藏列表
        private void LoadStockList()
        {
            string sql = @"SELECT s.stock_id, b.book_name, s.location, 
                                  CASE s.status 
                                      WHEN 0 THEN '可借' 
                                      WHEN 1 THEN '已借出' 
                                      WHEN 2 THEN '损坏' 
                                      WHEN 3 THEN '丢失' 
                                  END AS status_text,
                                  s.create_time
                           FROM book_stock s
                           LEFT JOIN books b ON s.book_id = b.book_id
                           ORDER BY s.stock_id DESC";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            dgvStock.DataSource = dt;

            if (dgvStock.Columns["stock_id"] != null)
                dgvStock.Columns["stock_id"].HeaderText = "馆藏ID";
            if (dgvStock.Columns["book_name"] != null)
                dgvStock.Columns["book_name"].HeaderText = "图书名称";
            if (dgvStock.Columns["location"] != null)
                dgvStock.Columns["location"].HeaderText = "馆藏位置";
            if (dgvStock.Columns["status_text"] != null)
                dgvStock.Columns["status_text"].HeaderText = "状态";
            if (dgvStock.Columns["create_time"] != null)
                dgvStock.Columns["create_time"].HeaderText = "入库时间";
        }
        #endregion

        #region 添加馆藏（检查库存限制）
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbBook.SelectedValue == null)
            {
                MessageBox.Show("请先添加图书数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbLocation.SelectedIndex < 0)
            {
                MessageBox.Show("请选择馆藏位置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(cmbBook.SelectedValue);
            string location = cmbLocation.SelectedItem?.ToString() ?? "";
            int status = cmbStatus.SelectedIndex >= 0 ? cmbStatus.SelectedIndex : 0;

            try
            {
                // 检查库存限制
                string checkSql = @"SELECT b.book_stock AS total_stock,
                                           IFNULL((SELECT COUNT(*) FROM book_stock WHERE book_id = b.book_id), 0) AS current_count,
                                           b.book_name
                                    FROM books b 
                                    WHERE b.book_id = @bookId";
                DataTable dt = MysqlHelper.ExecuteDataTable(checkSql, new MySqlParameter("@bookId", bookId));
                
                if (dt.Rows.Count > 0)
                {
                    int totalStock = Convert.ToInt32(dt.Rows[0]["total_stock"]);
                    int currentCount = Convert.ToInt32(dt.Rows[0]["current_count"]);
                    string bookName = dt.Rows[0]["book_name"]?.ToString() ?? "";

                    if (currentCount >= totalStock)
                    {
                        MessageBox.Show(
                            "无法添加馆藏！\n\n" +
                            "图书【" + bookName + "】\n" +
                            "库存数量：" + totalStock + " 本\n" +
                            "已入库数量：" + currentCount + " 本\n\n" +
                            "已达到库存上限，如需添加更多馆藏，请先修改图书库存数量。",
                            "库存不足",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    // 显示剩余可入库数量
                    int remaining = totalStock - currentCount - 1;
                    
                    // 执行添加
                    string sql = @"INSERT INTO book_stock (book_id, location, status)
                                   VALUES (@bookId, @location, @status)";
                    MySqlParameter[] param = {
                        new MySqlParameter("@bookId", bookId),
                        new MySqlParameter("@location", location),
                        new MySqlParameter("@status", status)
                    };

                    MysqlHelper.ExecuteNonQuery(sql, param);
                    
                    // 刷新列表
                    LoadBookList();
                    LoadStockList();
                    
                    string msg = "馆藏添加成功！";
                    if (remaining > 0)
                    {
                        msg += "\n\n该图书还可入库 " + remaining + " 本。";
                    }
                    else
                    {
                        msg += "\n\n⚠️ 该图书已全部入库完毕。";
                    }
                    
                    MessageBox.Show(msg, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 修改馆藏
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentStockId == -1)
            {
                MessageBox.Show("请先在表格中选中要修改的馆藏！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbBook.SelectedValue == null)
            {
                MessageBox.Show("请选择图书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbLocation.SelectedIndex < 0)
            {
                MessageBox.Show("请选择馆藏位置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(cmbBook.SelectedValue);
            string location = cmbLocation.SelectedItem?.ToString() ?? "";
            int status = cmbStatus.SelectedIndex >= 0 ? cmbStatus.SelectedIndex : 0;

            try
            {
                // 获取当前馆藏的原图书ID
                string getOldBookSql = "SELECT book_id FROM book_stock WHERE stock_id = @id";
                object oldBookIdObj = MysqlHelper.ExecuteScalar(getOldBookSql, new MySqlParameter("@id", currentStockId));
                int oldBookId = oldBookIdObj != null ? Convert.ToInt32(oldBookIdObj) : 0;

                // 如果更换了图书，需要检查新图书的库存限制
                if (bookId != oldBookId)
                {
                    string checkSql = @"SELECT b.book_stock AS total_stock,
                                               IFNULL((SELECT COUNT(*) FROM book_stock WHERE book_id = b.book_id), 0) AS current_count,
                                               b.book_name
                                        FROM books b 
                                        WHERE b.book_id = @bookId";
                    DataTable dt = MysqlHelper.ExecuteDataTable(checkSql, new MySqlParameter("@bookId", bookId));
                    
                    if (dt.Rows.Count > 0)
                    {
                        int totalStock = Convert.ToInt32(dt.Rows[0]["total_stock"]);
                        int currentCount = Convert.ToInt32(dt.Rows[0]["current_count"]);
                        string bookName = dt.Rows[0]["book_name"]?.ToString() ?? "";

                        if (currentCount >= totalStock)
                        {
                            MessageBox.Show(
                                "无法更换为此图书！\n\n" +
                                "图书【" + bookName + "】\n" +
                                "库存数量：" + totalStock + " 本\n" +
                                "已入库数量：" + currentCount + " 本\n\n" +
                                "已达到库存上限。",
                                "库存不足",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                string sql = @"UPDATE book_stock 
                               SET book_id = @bookId, location = @location, status = @status
                               WHERE stock_id = @id";
                MySqlParameter[] param = {
                    new MySqlParameter("@bookId", bookId),
                    new MySqlParameter("@location", location),
                    new MySqlParameter("@status", status),
                    new MySqlParameter("@id", currentStockId)
                };

                int rows = MysqlHelper.ExecuteNonQuery(sql, param);
                if (rows > 0)
                {
                    MessageBox.Show("馆藏信息修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookList();
                    LoadStockList();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("修改失败，未找到该馆藏数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 删除馆藏
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (currentStockId == -1 || dgvStock.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先在表格中选中要删除的馆藏！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (MySqlConnection conn = MysqlHelper.GetConnection())
                {
                    conn.Open();

                    // 检查是否有未归还的借阅记录
                    string checkBorrowed = @"SELECT COUNT(*) FROM borrow_records 
                                             WHERE stock_id = @id AND return_date IS NULL";
                    using (MySqlCommand cmd = new MySqlCommand(checkBorrowed, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentStockId);
                        int borrowedCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (borrowedCount > 0)
                        {
                            MessageBox.Show("无法删除！该馆藏当前正在被借阅中，请先归还后再删除。",
                                "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 统计历史借阅记录
                    string countBorrow = "SELECT COUNT(*) FROM borrow_records WHERE stock_id = @id";
                    int borrowRecordCount = 0;
                    using (MySqlCommand cmd = new MySqlCommand(countBorrow, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentStockId);
                        borrowRecordCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    string confirmMessage = "确定要删除该馆藏吗？\n\n";
                    if (borrowRecordCount > 0)
                    {
                        confirmMessage += "⚠️ 该馆藏存在 " + borrowRecordCount + " 条借阅记录，将一并删除！\n\n";
                    }
                    confirmMessage += "此操作不可恢复！";

                    if (MessageBox.Show(confirmMessage, "确认删除",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        return;
                    }

                    using (MySqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            if (borrowRecordCount > 0)
                            {
                                string deleteBorrow = "DELETE FROM borrow_records WHERE stock_id = @id";
                                using (MySqlCommand cmd = new MySqlCommand(deleteBorrow, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@id", currentStockId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            string deleteStock = "DELETE FROM book_stock WHERE stock_id = @id";
                            using (MySqlCommand cmd = new MySqlCommand(deleteStock, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@id", currentStockId);
                                cmd.ExecuteNonQuery();
                            }

                            trans.Commit();

                            MessageBox.Show("删除成功！", "删除成功",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadBookList();
                            LoadStockList();
                            ClearForm();
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
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 表格点击回填
        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvStock.Rows.Count)
            {
                ClearForm();
                return;
            }

            DataGridViewRow row = dgvStock.Rows[e.RowIndex];
            currentStockId = Convert.ToInt32(row.Cells["stock_id"].Value);

            cmbBook.Text = row.Cells["book_name"].Value?.ToString() ?? "";
            
            string locationText = row.Cells["location"].Value?.ToString() ?? "";
            int locationIndex = cmbLocation.FindStringExact(locationText);
            if (locationIndex >= 0)
            {
                cmbLocation.SelectedIndex = locationIndex;
            }
            else if (cmbLocation.Items.Count > 0)
            {
                cmbLocation.SelectedIndex = 0;
            }

            string statusText = row.Cells["status_text"].Value?.ToString() ?? "可借";
            cmbStatus.SelectedIndex = statusText switch
            {
                "可借" => 0,
                "已借出" => 1,
                "损坏" => 2,
                "丢失" => 3,
                _ => 0
            };
        }
        #endregion

        #region 清空表单
        private void ClearForm()
        {
            if (cmbBook.Items.Count > 0)
            {
                cmbBook.SelectedIndex = 0;
            }
            if (cmbLocation.Items.Count > 0)
            {
                cmbLocation.SelectedIndex = 0;
            }
            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }
            currentStockId = -1;
        }
        #endregion
    }
}