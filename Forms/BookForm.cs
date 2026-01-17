using LibrarySystem.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibrarySystem.Forms
{
    public partial class BookForm : Form
    {
        // 全局变量：存储当前选中的图书ID，用于修改和删除
        private int currentBookId = -1;

        public BookForm()
        {
            InitializeComponent();
        }

        #region 窗体加载事件：加载出版社下拉框 + 加载图书列表
        private void BookForm_Load(object sender, EventArgs e)
        {
            LoadPublishList();  // 加载出版社下拉框
            LoadBookList();     // 加载图书表格数据
        }
        #endregion

        #region 加载出版社下拉框数据（绑定图书的出版社）
        private void LoadPublishList()
        {
            string sql = "SELECT publish_id, publish_name FROM publishers ORDER BY publish_id ASC";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            cmbPublish.DataSource = dt;
            cmbPublish.DisplayMember = "publish_name";
            cmbPublish.ValueMember = "publish_id";
            cmbPublish.SelectedIndex = 0;
        }
        #endregion

        #region 加载图书列表（关联出版社表，显示出版社名称+ISBN，不是ID）
        private void LoadBookList()
        {
            string sql = @"SELECT b.book_id, b.isbn, b.book_name, b.book_author, p.publish_name, 
                                  b.book_price, b.book_stock, b.book_desc
                           FROM books b
                           LEFT JOIN publishers p ON b.publish_id = p.publish_id
                           ORDER BY b.book_id DESC";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            dgvBooks.DataSource = dt;

            // 设置表格中文表头，美化展示
            dgvBooks.Columns["book_id"].HeaderText = "图书编号";
            dgvBooks.Columns["isbn"].HeaderText = "ISBN号";
            dgvBooks.Columns["book_name"].HeaderText = "图书名称";
            dgvBooks.Columns["book_author"].HeaderText = "图书作者";
            dgvBooks.Columns["publish_name"].HeaderText = "出版社";
            dgvBooks.Columns["book_price"].HeaderText = "图书价格";
            dgvBooks.Columns["book_stock"].HeaderText = "库存数量";
            dgvBooks.Columns["book_desc"].HeaderText = "图书简介";
        }
        #endregion

        #region 添加图书按钮事件 - 完整校验+参数化SQL（增加ISBN）
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. 必填项校验
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("图书名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("图书作者不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAuthor.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("图书ISBN号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtISBN.Focus();
                return;
            }

            // 2. 数字类型校验（价格、库存）
            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("图书价格请输入有效的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("库存数量请输入有效的非负整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            try
            {
                // 3. 参数化添加SQL，防止SQL注入，增加ISBN字段
                string sql = @"INSERT INTO books (isbn, book_name, book_author, publish_id, book_price, book_stock, book_desc)
                               VALUES (@isbn, @name, @author, @pid, @price, @stock, @desc)";
                MySqlParameter[] param = {
                    new MySqlParameter("@isbn", txtISBN.Text.Trim()),
                    new MySqlParameter("@name", txtName.Text.Trim()),
                    new MySqlParameter("@author", txtAuthor.Text.Trim()),
                    new MySqlParameter("@pid", cmbPublish.SelectedValue),
                    new MySqlParameter("@price", price),
                    new MySqlParameter("@stock", stock),
                    new MySqlParameter("@desc", txtDesc.Text.Trim())
                };

                MysqlHelper.ExecuteNonQuery(sql, param);
                LoadBookList();
                MessageBox.Show("图书添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm(); // 清空表单，方便继续添加
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Duplicate entry"))
                {
                    MessageBox.Show("添加失败：该ISBN号已存在，请勿重复录入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("添加失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 修改图书按钮事件 - 完整逻辑（增加ISBN）
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // 1. 校验是否选中图书
            if (currentBookId == -1)
            {
                MessageBox.Show("请先在表格中选中要修改的图书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. 必填项校验
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("图书名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("图书作者不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAuthor.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("图书ISBN号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtISBN.Focus();
                return;
            }

            // 3. 数字类型校验
            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("图书价格请输入有效的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("库存数量请输入有效的非负整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            try
            {
                // 4. 参数化修改SQL，增加ISBN字段
                string sql = @"UPDATE books 
                               SET isbn = @isbn, book_name = @name, book_author = @author, publish_id = @pid,
                                   book_price = @price, book_stock = @stock, book_desc = @desc
                               WHERE book_id = @id";
                MySqlParameter[] param = {
                    new MySqlParameter("@isbn", txtISBN.Text.Trim()),
                    new MySqlParameter("@name", txtName.Text.Trim()),
                    new MySqlParameter("@author", txtAuthor.Text.Trim()),
                    new MySqlParameter("@pid", cmbPublish.SelectedValue),
                    new MySqlParameter("@price", price),
                    new MySqlParameter("@stock", stock),
                    new MySqlParameter("@desc", txtDesc.Text.Trim()),
                    new MySqlParameter("@id", currentBookId)
                };

                int rows = MysqlHelper.ExecuteNonQuery(sql, param);
                if (rows > 0)
                {
                    MessageBox.Show("图书信息修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookList();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("修改失败，未找到该图书数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    MessageBox.Show("修改失败：该ISBN号已存在，请勿重复录入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("修改失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 删除图书按钮事件 - 带二次确认+关联校验
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (currentBookId == -1 || dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先在表格中选中要删除的图书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("确定删除该图书吗？删除后相关借阅数据将关联失效！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM books WHERE book_id = @id";
                    MysqlHelper.ExecuteNonQuery(sql, new MySqlParameter("@id", currentBookId));
                    LoadBookList();
                    MessageBox.Show("图书删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败：该图书存在关联借阅记录，无法删除!\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 表格点击事件 - 回填表单数据（处理NULL值+下拉框精准匹配+ISBN）
        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvBooks.Rows.Count)
            {
                ClearForm();
                return;
            }

            DataGridViewRow row = dgvBooks.Rows[e.RowIndex];
            currentBookId = Convert.ToInt32(row.Cells["book_id"].Value);

            // 回填表单数据，处理数据库NULL值，防止崩溃
            txtISBN.Text = row.Cells["isbn"].Value.ToString();
            txtName.Text = row.Cells["book_name"].Value.ToString();
            txtAuthor.Text = row.Cells["book_author"].Value.ToString();
            txtPrice.Text = row.Cells["book_price"].Value.ToString();
            txtStock.Text = row.Cells["book_stock"].Value.ToString();
            txtDesc.Text = row.Cells["book_desc"].Value == DBNull.Value ? "" : row.Cells["book_desc"].Value.ToString();
            
            // 精准回填出版社下拉框
            string publishName = row.Cells["publish_name"].Value.ToString();
            cmbPublish.Text = publishName;
        }
        #endregion

        #region 公共方法：清空表单+重置选中状态
        private void ClearForm()
        {
            txtISBN.Clear();
            txtName.Clear();
            txtAuthor.Clear();
            txtPrice.Text = "0.00";
            txtStock.Text = "0";
            txtDesc.Clear();
            cmbPublish.SelectedIndex = 0;
            currentBookId = -1;
        }
        #endregion
    }
}