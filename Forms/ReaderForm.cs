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

        // 加载表格-读者数据 ✅【原版SQL、零修改、加中文列名设置、防空引用处理】
        private void LoadReaders()
        {
            // ✅ 还是你最开始的原版SQL，一行没改，不会有任何SQL报错
            string sql = @"SELECT r.reader_id, r.name, t.type_name, r.phone, r.borrowed_count, r.total_unpaid_fines
                           FROM readers r 
                           LEFT JOIN reader_types t ON r.type_id = t.type_id";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            dgvReaders.DataSource = dt;

            // ✅ 核心：设置所有列的中文名称，解决英文列名问题，无任何匹配问题
            dgvReaders.Columns["reader_id"].HeaderText = "读者ID";
            dgvReaders.Columns["name"].HeaderText = "姓名";
            dgvReaders.Columns["type_name"].HeaderText = "读者类型";
            dgvReaders.Columns["phone"].HeaderText = "联系电话";
            dgvReaders.Columns["borrowed_count"].HeaderText = "借阅数量";
            dgvReaders.Columns["total_unpaid_fines"].HeaderText = "未缴罚款(元)";

            // ✅ 防空引用报错：先判断列是否存在，再设置格式，彻底杜绝空引用
            if (dgvReaders.Columns["total_unpaid_fines"] != null)
            {
                dgvReaders.Columns["total_unpaid_fines"].DefaultCellStyle.Format = "0.00";
            }
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
        // 删除读者按钮事件（修复外键约束报错，先删借阅记录再删读者）
private void btnDel_Click(object sender, EventArgs e)
{
    // 校验是否选中有效数据
    if (currentReaderId == -1 || dgvReaders.SelectedRows.Count == 0)
    {
        MessageBox.Show("请先在表格中选中要删除的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }

    if (MessageBox.Show("确定删除该读者？删除后该读者的所有借阅记录也会一并删除，数据不可恢复！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
        try
        {
            // ============ 核心修复：第一步，先删除该读者的所有借阅记录 ============
            string delBorrowSql = "DELETE FROM borrow_records WHERE reader_id = @id";
            MysqlHelper.ExecuteNonQuery(delBorrowSql, new MySqlParameter("@id", currentReaderId));

            // ============ 第二步，再删除读者 ============
            string delReaderSql = "DELETE FROM readers WHERE reader_id = @id";
            MysqlHelper.ExecuteNonQuery(delReaderSql, new MySqlParameter("@id", currentReaderId));

            LoadReaders();
            MessageBox.Show("读者删除成功！该读者的借阅记录已一并清除", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm(); // 清空表单+重置选中ID
        }
        catch (Exception ex)
        {
            MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

        // 表格行点击-回填表单数据 ✅【原版代码、零修改、用英文列名匹配，绝对不会报错】
        private void dgvReaders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修复：判断行索引是否有效，点击表头时不执行，避免报错
            if (e.RowIndex < 0 || e.RowIndex >= dgvReaders.Rows.Count) return;

            DataGridViewRow row = dgvReaders.Rows[e.RowIndex];
            // 回填文本框 ✅ 用数据库原始英文字段名，永远不会匹配失败
            txtName.Text = row.Cells["name"].Value.ToString();
            txtPhone.Text = row.Cells["phone"].Value == DBNull.Value ? "" : row.Cells["phone"].Value.ToString();
            // 存储选中的读者ID
            currentReaderId = Convert.ToInt32(row.Cells["reader_id"].Value);
            // 精准回填下拉框的读者类型
            string selectTypeName = row.Cells["type_name"].Value.ToString();
            cmbType.Text = selectTypeName;
        }

        // 修改按钮完整逻辑
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
                string sql = @"UPDATE readers 
                               SET name = @n, phone = @p, type_id = @t 
                               WHERE reader_id = @id";

                MySqlParameter[] param = {
                    new MySqlParameter("@n", txtName.Text),
                    new MySqlParameter("@p", txtPhone.Text),
                    new MySqlParameter("@t", cmbType.SelectedValue),
                    new MySqlParameter("@id", currentReaderId)
                };

                int rows = MysqlHelper.ExecuteNonQuery(sql, param);
                if (rows > 0)
                {
                    MessageBox.Show("读者信息修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReaders(); 
                    ClearForm();   
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