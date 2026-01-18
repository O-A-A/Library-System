using LibrarySystem.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibrarySystem.Forms
{
    public partial class PublishForm : Form
    {
        // 全局变量：存储当前选中的出版社ID，用于修改/删除，默认-1表示未选中
        private int currentPublishId = -1;

        public PublishForm()
        {
            InitializeComponent();
        }

        #region 窗体加载事件：初始化加载出版社数据
        private void PublishForm_Load(object sender, EventArgs e)
        {
            LoadPublishData();
        }
        #endregion

        #region 核心方法：加载出版社表格数据
        private void LoadPublishData()
        {
            string sql = @"SELECT publish_id, publish_name, publish_address, publish_phone, publish_desc 
                           FROM publishers 
                           ORDER BY publish_id DESC";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            dgvPublishers.DataSource = dt;

            // 设置中文表头
            if (dgvPublishers.Columns["publish_id"] != null)
                dgvPublishers.Columns["publish_id"].HeaderText = "序号";
            if (dgvPublishers.Columns["publish_name"] != null)
                dgvPublishers.Columns["publish_name"].HeaderText = "出版社名称";
            if (dgvPublishers.Columns["publish_address"] != null)
                dgvPublishers.Columns["publish_address"].HeaderText = "出版社地址";
            if (dgvPublishers.Columns["publish_phone"] != null)
                dgvPublishers.Columns["publish_phone"].HeaderText = "联系电话";
            if (dgvPublishers.Columns["publish_desc"] != null)
                dgvPublishers.Columns["publish_desc"].HeaderText = "出版社简介";
        }
        #endregion

        #region 添加出版社按钮事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("出版社名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                string sql = @"INSERT INTO publishers (publish_name, publish_address, publish_phone, publish_desc) 
                               VALUES (@name, @addr, @phone, @desc)";
                MySqlParameter[] param = {
                    new MySqlParameter("@name", txtName.Text.Trim()),
                    new MySqlParameter("@addr", txtAddress.Text.Trim()),
                    new MySqlParameter("@phone", txtPhone.Text.Trim()),
                    new MySqlParameter("@desc", txtDesc.Text.Trim())
                };

                MysqlHelper.ExecuteNonQuery(sql, param);
                LoadPublishData();
                MessageBox.Show("出版社添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 删除出版社按钮事件（处理外键约束）
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (currentPublishId == -1 || dgvPublishers.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先在表格中选中要删除的出版社！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string publishName = txtName.Text;

            try
            {
                using (MySqlConnection conn = MysqlHelper.GetConnection())
                {
                    conn.Open();

                    // 检查是否有关联的图书
                    string checkBooks = "SELECT COUNT(*) FROM books WHERE publish_id = @id";
                    int bookCount = 0;
                    using (MySqlCommand cmd = new MySqlCommand(checkBooks, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentPublishId);
                        bookCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 如果有关联图书，提示用户选择处理方式
                    if (bookCount > 0)
                    {
                        // 获取关联图书的详细信息
                        string getBooks = "SELECT book_name FROM books WHERE publish_id = @id LIMIT 5";
                        string bookList = "";
                        using (MySqlCommand cmd = new MySqlCommand(getBooks, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", currentPublishId);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    bookList += "\n  - " + reader.GetString("book_name");
                                }
                            }
                        }
                        if (bookCount > 5)
                        {
                            bookList += "\n  ... 等共 " + bookCount + " 本图书";
                        }

                        // 询问用户是否强制删除
                        DialogResult result = MessageBox.Show(
                            "无法直接删除！\n\n" +
                            "出版社【" + publishName + "】关联了 " + bookCount + " 本图书：" + bookList + "\n\n" +
                            "请选择处理方式：\n" +
                            "【是】- 将关联图书的出版社设为\"未知出版社\"后删除\n" +
                            "【否】- 取消删除，手动处理关联图书",
                            "存在关联图书",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result != DialogResult.Yes)
                        {
                            return;
                        }

                        // 用户选择强制删除：先处理关联图书
                        using (MySqlTransaction trans = conn.BeginTransaction())
                        {
                            try
                            {
                                // 检查是否存在"未知出版社"
                                string checkUnknown = "SELECT publish_id FROM publishers WHERE publish_name = '未知出版社'";
                                int unknownPublishId = 0;
                                using (MySqlCommand cmd = new MySqlCommand(checkUnknown, conn, trans))
                                {
                                    object result2 = cmd.ExecuteScalar();
                                    if (result2 != null && result2 != DBNull.Value)
                                    {
                                        unknownPublishId = Convert.ToInt32(result2);
                                    }
                                }

                                // 如果不存在"未知出版社"，创建一个
                                if (unknownPublishId == 0)
                                {
                                    string insertUnknown = @"INSERT INTO publishers (publish_name, publish_desc) 
                                                             VALUES ('未知出版社', '用于存放原出版社被删除的图书')";
                                    using (MySqlCommand cmd = new MySqlCommand(insertUnknown, conn, trans))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                    using (MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn, trans))
                                    {
                                        unknownPublishId = Convert.ToInt32(cmd.ExecuteScalar());
                                    }
                                }

                                // 将关联图书转移到"未知出版社"
                                string updateBooks = "UPDATE books SET publish_id = @newId WHERE publish_id = @oldId";
                                using (MySqlCommand cmd = new MySqlCommand(updateBooks, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@newId", unknownPublishId);
                                    cmd.Parameters.AddWithValue("@oldId", currentPublishId);
                                    cmd.ExecuteNonQuery();
                                }

                                // 删除出版社
                                string deletePublish = "DELETE FROM publishers WHERE publish_id = @id";
                                using (MySqlCommand cmd = new MySqlCommand(deletePublish, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@id", currentPublishId);
                                    cmd.ExecuteNonQuery();
                                }

                                trans.Commit();

                                MessageBox.Show(
                                    "删除成功！\n\n" +
                                    "已删除出版社【" + publishName + "】\n" +
                                    "其关联的 " + bookCount + " 本图书已转移至【未知出版社】",
                                    "删除成功",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                LoadPublishData();
                                ClearForm();
                            }
                            catch
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    else
                    {
                        // 没有关联图书，直接确认删除
                        DialogResult dialogResult = MessageBox.Show(
                            "确定要删除出版社【" + publishName + "】吗？\n\n此操作不可恢复！",
                            "确认删除",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (dialogResult != DialogResult.Yes)
                            return;

                        string deletePublish = "DELETE FROM publishers WHERE publish_id = @id";
                        using (MySqlCommand cmd = new MySqlCommand(deletePublish, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", currentPublishId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show(
                            "删除成功！\n\n已删除出版社【" + publishName + "】",
                            "删除成功",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LoadPublishData();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 修改出版社按钮事件
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentPublishId == -1)
            {
                MessageBox.Show("请先在表格中选中要修改的出版社！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("出版社名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                string sql = @"UPDATE publishers 
                               SET publish_name = @name, publish_address = @addr, 
                                   publish_phone = @phone, publish_desc = @desc 
                               WHERE publish_id = @id";
                MySqlParameter[] param = {
                    new MySqlParameter("@name", txtName.Text.Trim()),
                    new MySqlParameter("@addr", txtAddress.Text.Trim()),
                    new MySqlParameter("@phone", txtPhone.Text.Trim()),
                    new MySqlParameter("@desc", txtDesc.Text.Trim()),
                    new MySqlParameter("@id", currentPublishId)
                };

                int rows = MysqlHelper.ExecuteNonQuery(sql, param);
                if (rows > 0)
                {
                    MessageBox.Show("出版社信息修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPublishData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("修改失败，未找到该出版社数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 表格点击事件：回填表单数据
        private void dgvPublishers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPublishers.Rows.Count)
            {
                ClearForm();
                return;
            }

            DataGridViewRow row = dgvPublishers.Rows[e.RowIndex];
            currentPublishId = Convert.ToInt32(row.Cells["publish_id"].Value);
            
            txtName.Text = row.Cells["publish_name"].Value?.ToString() ?? "";
            txtAddress.Text = row.Cells["publish_address"].Value == DBNull.Value ? "" : row.Cells["publish_address"].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells["publish_phone"].Value == DBNull.Value ? "" : row.Cells["publish_phone"].Value?.ToString() ?? "";
            txtDesc.Text = row.Cells["publish_desc"].Value == DBNull.Value ? "" : row.Cells["publish_desc"].Value?.ToString() ?? "";
        }
        #endregion

        #region 清空表单
        private void ClearForm()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtDesc.Clear();
            currentPublishId = -1;
        }
        #endregion
    }
}