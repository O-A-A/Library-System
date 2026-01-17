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
            LoadBookList();   // 加载图书下拉框
            LoadStockList();  // 加载馆藏列表
        }
        #endregion

        #region 加载图书下拉框（关联books表）
        private void LoadBookList()
        {
            string sql = "SELECT book_id, book_name FROM books ORDER BY book_name ASC";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            cmbBook.DataSource = dt;
            cmbBook.DisplayMember = "book_name";
            cmbBook.ValueMember = "book_id";
            cmbBook.SelectedIndex = 0;
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

            // 设置表头
            dgvStock.Columns["stock_id"].HeaderText = "馆藏ID";
            dgvStock.Columns["book_name"].HeaderText = "图书名称";
            dgvStock.Columns["location"].HeaderText = "馆藏位置";
            dgvStock.Columns["status_text"].HeaderText = "状态";
            dgvStock.Columns["create_time"].HeaderText = "入库时间";
        }
        #endregion

        #region 添加馆藏
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int bookId = Convert.ToInt32(cmbBook.SelectedValue);
            string location = txtLocation.Text.Trim();
            int status = cmbStatus.SelectedIndex;

            try
            {
                string sql = @"INSERT INTO book_stock (book_id, location, status)
                               VALUES (@bookId, @location, @status)";
                MySqlParameter[] param = {
                    new MySqlParameter("@bookId", bookId),
                    new MySqlParameter("@location", location),
                    new MySqlParameter("@status", status)
                };

                MysqlHelper.ExecuteNonQuery(sql, param);
                LoadStockList();
                MessageBox.Show("馆藏添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
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

            int bookId = Convert.ToInt32(cmbBook.SelectedValue);
            string location = txtLocation.Text.Trim();
            int status = cmbStatus.SelectedIndex;

            try
            {
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

            if (MessageBox.Show("确定删除该馆藏吗？删除后相关借阅记录将受影响！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM book_stock WHERE stock_id = @id";
                    MysqlHelper.ExecuteNonQuery(sql, new MySqlParameter("@id", currentStockId));
                    LoadStockList();
                    MessageBox.Show("馆藏删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败：该馆藏存在关联借阅记录，无法删除!\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

            // 回填表单
            cmbBook.Text = row.Cells["book_name"].Value.ToString();
            txtLocation.Text = row.Cells["location"].Value.ToString();
            string statusText = row.Cells["status_text"].Value.ToString();
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
            cmbBook.SelectedIndex = 0;
            txtLocation.Text = "一楼社科区";
            cmbStatus.SelectedIndex = 0;
            currentStockId = -1;
        }
        #endregion
    }
}