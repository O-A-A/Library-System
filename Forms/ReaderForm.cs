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
            string sql = @"SELECT r.reader_id, r.name, t.type_name, r.phone, r.borrowed_count, r.total_unpaid_fines
                           FROM readers r 
                           LEFT JOIN reader_types t ON r.type_id = t.type_id";
            DataTable dt = MysqlHelper.ExecuteDataTable(sql);
            dgvReaders.DataSource = dt;

            // 设置所有列的中文名称
            dgvReaders.Columns["reader_id"].HeaderText = "读者ID";
            dgvReaders.Columns["name"].HeaderText = "姓名";
            dgvReaders.Columns["type_name"].HeaderText = "读者类型";
            dgvReaders.Columns["phone"].HeaderText = "联系电话";
            dgvReaders.Columns["borrowed_count"].HeaderText = "借阅数量";
            dgvReaders.Columns["total_unpaid_fines"].HeaderText = "未缴罚款(元)";

            // 防空引用报错
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
            LoadReaders();
            MessageBox.Show("读者添加成功！");
            ClearForm();
        }

        // 删除读者按钮事件（完整修复版：处理所有外键约束 + 详细提示）
        private void btnDel_Click(object sender, EventArgs e)
        {
            // 校验是否选中有效数据
            if (currentReaderId == -1 || dgvReaders.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先在表格中选中要删除的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string readerName = txtName.Text;

            try
            {
                using (MySqlConnection conn = MysqlHelper.GetConnection())
                {
                    conn.Open();

                    // 1. 检查是否有未归还的图书
                    string checkBorrowed = @"SELECT COUNT(*) FROM borrow_records 
                                             WHERE reader_id = @id AND return_date IS NULL";
                    using (MySqlCommand cmd = new MySqlCommand(checkBorrowed, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentReaderId);
                        int borrowedCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (borrowedCount > 0)
                        {
                            MessageBox.Show(
                                $"❌ 无法删除！\n\n" +
                                $"读者【{readerName}】还有 {borrowedCount} 本书未归还，\n" +
                                $"请先归还图书再删除。",
                                "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 2. 检查是否有未缴罚款
                    string checkFines = "SELECT total_unpaid_fines FROM readers WHERE reader_id = @id";
                    decimal unpaidFines = 0;
                    using (MySqlCommand cmd = new MySqlCommand(checkFines, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentReaderId);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            unpaidFines = Convert.ToDecimal(result);
                        }
                    }
                    if (unpaidFines > 0)
                    {
                        MessageBox.Show(
                            $"❌ 无法删除！\n\n" +
                            $"读者【{readerName}】还有未缴罚款 {unpaidFines:F2} 元，\n" +
                            $"请先缴清罚款再删除。",
                            "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. 统计关联数据数量
                    int finePaymentCount = 0;
                    int borrowRecordCount = 0;

                    string countPayment = "SELECT COUNT(*) FROM fine_payment WHERE reader_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(countPayment, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentReaderId);
                        finePaymentCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    string countBorrow = "SELECT COUNT(*) FROM borrow_records WHERE reader_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(countBorrow, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentReaderId);
                        borrowRecordCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 4. 显示确认对话框
                    string confirmMessage = $"确定要删除读者【{readerName}】吗？\n\n";
                    if (borrowRecordCount > 0 || finePaymentCount > 0)
                    {
                        confirmMessage += "⚠️ 该读者存在以下历史记录，将一并删除：\n";
                        if (borrowRecordCount > 0)
                            confirmMessage += $"  • 借阅记录：{borrowRecordCount} 条\n";
                        if (finePaymentCount > 0)
                            confirmMessage += $"  • 缴费记录：{finePaymentCount} 条\n";
                        confirmMessage += "\n此操作不可恢复！";
                    }

                    DialogResult dialogResult = MessageBox.Show(confirmMessage, "确认删除",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogResult != DialogResult.Yes)
                        return;

                    // 5. 开始事务删除（按顺序：缴费记录 → 借阅记录 → 读者）
                    using (MySqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 第一步：删除缴费记录
                            if (finePaymentCount > 0)
                            {
                                string deleteFinePayment = "DELETE FROM fine_payment WHERE reader_id = @id";
                                using (MySqlCommand cmd = new MySqlCommand(deleteFinePayment, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@id", currentReaderId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // 第二步：删除借阅记录
                            if (borrowRecordCount > 0)
                            {
                                string deleteBorrowRecords = "DELETE FROM borrow_records WHERE reader_id = @id";
                                using (MySqlCommand cmd = new MySqlCommand(deleteBorrowRecords, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@id", currentReaderId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // 第三步：删除读者
                            string deleteReader = "DELETE FROM readers WHERE reader_id = @id";
                            using (MySqlCommand cmd = new MySqlCommand(deleteReader, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@id", currentReaderId);
                                cmd.ExecuteNonQuery();
                            }

                            trans.Commit();

                            // 显示成功消息
                            string successMessage = $"✅ 删除成功！\n\n已删除读者【{readerName}】";
                            if (borrowRecordCount > 0 || finePaymentCount > 0)
                            {
                                successMessage += "\n\n同时删除了：";
                                if (borrowRecordCount > 0)
                                    successMessage += $"\n  • 借阅记录 {borrowRecordCount} 条";
                                if (finePaymentCount > 0)
                                    successMessage += $"\n  • 缴费记录 {finePaymentCount} 条";
                            }

                            MessageBox.Show(successMessage, "删除成功",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadReaders();
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
                MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 表格行点击-回填表单数据
        private void dgvReaders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断行索引是否有效，点击表头时不执行
            if (e.RowIndex < 0 || e.RowIndex >= dgvReaders.Rows.Count) return;

            DataGridViewRow row = dgvReaders.Rows[e.RowIndex];
            txtName.Text = row.Cells["name"].Value.ToString();
            txtPhone.Text = row.Cells["phone"].Value == DBNull.Value ? "" : row.Cells["phone"].Value.ToString();
            currentReaderId = Convert.ToInt32(row.Cells["reader_id"].Value);
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

        // 封装：清空表单+重置选中状态
        private void ClearForm()
        {
            txtName.Clear();
            txtPhone.Clear();
            cmbType.SelectedIndex = 0;
            currentReaderId = -1;
        }
    }
}