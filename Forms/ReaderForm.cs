using LibrarySystem.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibrarySystem.Forms
{
    public partial class ReaderForm : Form
    {
        // 全局变量：存储当前选中的读者ID，用于修改和删除，默认-1表示未选中
        private int currentReaderId = -1;

        public ReaderForm()
        {
            InitializeComponent();
        }

        private void ReaderForm_Load(object sender, EventArgs e)
        {
            LoadReaderTypes();
            LoadReaders();
        }

        // 加载下拉框-读者类型数据
        private void LoadReaderTypes()
        {
            string sql = "SELECT type_id, type_name FROM reader_types";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            cmbType.DataSource = dt;
            cmbType.DisplayMember = "type_name";
            cmbType.ValueMember = "type_id";
        }

        // 加载表格-读者数据
        private void LoadReaders()
        {
            string sql = @"SELECT r.reader_id, r.name, t.type_name, r.phone, r.borrowed_count 
                           FROM readers r 
                           LEFT JOIN reader_types t ON r.type_id = t.type_id";
            dgvReaders.DataSource = MysqlHelper.ExecuteDataTable(sql);
        }

        // 添加读者按钮事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 校验姓名不能为空
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("读者姓名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            string sql = "INSERT INTO readers (name, phone, type_id) VALUES (@n, @p, @t)";
            MySqlParameter[] param = {
                new MySqlParameter("@n", txtName.Text),
                new MySqlParameter("@p", txtPhone.Text),
                new MySqlParameter("@t", cmbType.SelectedValue)
            };

            MysqlHelper.ExecuteNonQuery(sql, param);
            LoadReaders(); // 刷新表格
            MessageBox.Show("读者添加成功！");
            
            // 优化：添加成功后清空表单，方便继续新增
            ClearForm();
        }

        // 删除读者按钮事件（优化增强版）
        private void btnDel_Click(object sender, EventArgs e)
        {
            // 校验是否选中有效数据
            if (currentReaderId == -1 || dgvReaders.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先在表格中选中要删除的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("确定删除该读者？删除后数据不可恢复！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "DELETE FROM readers WHERE reader_id = @id";
                MysqlHelper.ExecuteNonQuery(sql, new MySqlParameter("@id", currentReaderId));
                LoadReaders();
                MessageBox.Show("读者删除成功！");
                ClearForm(); // 清空表单+重置选中ID
            }
        }

        // 表格行点击-回填表单数据（核心修复+优化）
        private void dgvReaders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修复：判断行索引是否有效，点击表头时不执行，避免报错
            if (e.RowIndex < 0 || e.RowIndex >= dgvReaders.Rows.Count) return;

            DataGridViewRow row = dgvReaders.Rows[e.RowIndex];
            // 回填文本框
            txtName.Text = row.Cells["name"].Value.ToString();
            txtPhone.Text = row.Cells["phone"].Value == DBNull.Value ? "" : row.Cells["phone"].Value.ToString();
            // 存储选中的读者ID
            currentReaderId = Convert.ToInt32(row.Cells["reader_id"].Value);
            // 精准回填下拉框的读者类型
            string selectTypeName = row.Cells["type_name"].Value.ToString();
            cmbType.Text = selectTypeName;
        }

        // ✅ 你的修改按钮【btnEdit】完整逻辑 - 核心实现
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // 1. 校验：是否选中要修改的读者
            if (currentReaderId == -1)
            {
                MessageBox.Show("请先在表格中选中要修改的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. 校验：姓名不能为空
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("读者姓名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                // 3. 修改的参数化SQL语句，防止SQL注入
                string sql = @"UPDATE readers 
                               SET name = @n, phone = @p, type_id = @t 
                               WHERE reader_id = @id";

                MySqlParameter[] param = {
                    new MySqlParameter("@n", txtName.Text),
                    new MySqlParameter("@p", txtPhone.Text),
                    new MySqlParameter("@t", cmbType.SelectedValue),
                    new MySqlParameter("@id", currentReaderId)
                };

                // 4. 执行修改并获取受影响行数
                int rows = MysqlHelper.ExecuteNonQuery(sql, param);
                if (rows > 0)
                {
                    MessageBox.Show("读者信息修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReaders(); // 刷新表格生效
                    ClearForm();   // 清空表单+重置选中ID
                }
                else
                {
                    MessageBox.Show("修改失败，未找到该读者数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改异常：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 封装：清空表单+重置选中状态 公共方法
        private void ClearForm()
        {
            txtName.Clear();
            txtPhone.Clear();
            cmbType.SelectedIndex = 0;
            currentReaderId = -1;
        }
    }
}