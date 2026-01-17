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
            LoadPublishData(); // 加载表格数据
        }
        #endregion

        #region 核心方法：加载出版社表格数据
        private void LoadPublishData()
        {
            // 查询出版社所有数据，排序：最新添加的在前面
            string sql = @"SELECT publish_id, publish_name, publish_address, publish_phone, publish_desc 
                           FROM publishers 
                           ORDER BY publish_id DESC";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            dgvPublishers.DataSource = dt;

            // 可选：给表格列设置中文表头（美化，可删除）
            dgvPublishers.Columns["publish_id"].HeaderText = "序号";
            dgvPublishers.Columns["publish_name"].HeaderText = "出版社名称";
            dgvPublishers.Columns["publish_address"].HeaderText = "出版社地址";
            dgvPublishers.Columns["publish_phone"].HeaderText = "联系电话";
            dgvPublishers.Columns["publish_desc"].HeaderText = "出版社简介";
        }
        #endregion

        #region 添加出版社按钮事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. 数据校验：出版社名称为必填项，不能为空
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("出版社名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                // 2. 参数化SQL 新增语句，防止SQL注入，和你的读者管理规范一致
                string sql = @"INSERT INTO publishers (publish_name, publish_address, publish_phone, publish_desc) 
                               VALUES (@name, @addr, @phone, @desc)";
                MySqlParameter[] param = {
                    new MySqlParameter("@name", txtName.Text.Trim()),
                    new MySqlParameter("@addr", txtAddress.Text.Trim()),
                    new MySqlParameter("@phone", txtPhone.Text.Trim()),
                    new MySqlParameter("@desc", txtDesc.Text.Trim())
                };

                // 3. 执行新增并刷新表格
                MysqlHelper.ExecuteNonQuery(sql, param);
                LoadPublishData();
                MessageBox.Show("出版社添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm(); // 清空表单，方便继续添加
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 删除出版社按钮事件
        private void btnDel_Click(object sender, EventArgs e)
        {
            // 1. 校验：是否选中了要删除的出版社
            if (currentPublishId == -1 || dgvPublishers.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先在表格中选中要删除的出版社！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. 二次确认删除，防止误操作
            if (MessageBox.Show("确定删除该出版社吗？删除后相关数据将关联失效！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM publishers WHERE publish_id = @id";
                    MysqlHelper.ExecuteNonQuery(sql, new MySqlParameter("@id", currentPublishId));
                    LoadPublishData();
                    MessageBox.Show("出版社删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm(); // 清空表单+重置选中ID
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败：该出版社存在关联图书，无法删除!\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 修改出版社按钮事件
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // 1. 校验：是否选中要修改的出版社
            if (currentPublishId == -1)
            {
                MessageBox.Show("请先在表格中选中要修改的出版社！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. 校验：出版社名称不能为空
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("出版社名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                // 3. 参数化修改SQL语句
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

                // 4. 执行修改并刷新
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
                MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 表格点击事件：回填表单数据（修改前置）
        private void dgvPublishers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修复：点击表头不执行，防止索引越界报错
            if (e.RowIndex < 0 || e.RowIndex >= dgvPublishers.Rows.Count)
            {
                ClearForm();
                return;
            }

            // 获取选中行数据
            DataGridViewRow row = dgvPublishers.Rows[e.RowIndex];
            currentPublishId = Convert.ToInt32(row.Cells["publish_id"].Value);
            // 回填表单，处理数据库NULL值，防止程序崩溃
            txtName.Text = row.Cells["publish_name"].Value.ToString();
            txtAddress.Text = row.Cells["publish_address"].Value == DBNull.Value ? "" : row.Cells["publish_address"].Value.ToString();
            txtPhone.Text = row.Cells["publish_phone"].Value == DBNull.Value ? "" : row.Cells["publish_phone"].Value.ToString();
            txtDesc.Text = row.Cells["publish_desc"].Value == DBNull.Value ? "" : row.Cells["publish_desc"].Value.ToString();
        }
        #endregion

        #region 公共方法：清空表单+重置选中状态
        private void ClearForm()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtDesc.Clear();
            currentPublishId = -1; // 重置选中ID
        }
        #endregion
    }
}