using MySql.Data.MySqlClient;
using System;
using System.Data;
namespace LibrarySystem.Helpers
{
    public class MysqlHelper
    {
        // ！！！请务必修改这里的数据库连接信息！！！
        private static string connStr = "server=localhost;port=3306;user id=root;password=306770407ZJF;database=LibraryDB;charset=utf8";
      
        // 获取连接对象（用于事务）
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connStr);
        }

        // 执行增删改
        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // 执行查询返回 DataTable
        public static DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // 执行查询返回首行首列 (用于登录验证、获取ID等)
        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}