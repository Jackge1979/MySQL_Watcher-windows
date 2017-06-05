using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MySQLWatcher
{
    /// <summary>
    /// 数据库操作类
    /// author: Jack R. Ge(jack_r_ge@126.com)
    /// </summary>
    public class SQLHelper
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\mysqladvisor";

        private static MySqlConnection conn;

        private static string host = "";
        private static string user = "";
        private static string passwd = "";
        private static string port = "3306";
        private static string db = "";
        private static string operuser = "";
        private static string operpwd = "";

        /// <summary>
        /// 返回配置文件中指定的连接
        /// </summary>
        /// <returns>配置文件中指定的连接</returns>
        private static void GetConnection()
        {
            host = OperateIniFile.ReadIniData("database", "host", "", path + @"\dbset.ini");
            user = OperateIniFile.ReadIniData("database", "user", "", path + @"\dbset.ini");
            db = OperateIniFile.ReadIniData("database", "db", "", path + @"\dbset.ini");
            passwd = OperateIniFile.ReadIniData("database", "passwd", "", path + @"\dbset.ini");
            port = OperateIniFile.ReadIniData("database", "port", "", path + @"\dbset.ini");
            operuser = OperateIniFile.ReadIniData("database", "operuser", "", path + @"\dbset.ini");
            operpwd = OperateIniFile.ReadIniData("database", "operpwd", "", path + @"\dbset.ini");
            string connNewString = "Database='" + db + "';Data Source='" + host + "';User Id='" + user + "';Password='" + passwd + "';Port=" + port + ";charset='utf8';pooling=true;Allow Zero Datetime=True";

            if (conn == null)
            {
                conn = new MySqlConnection(connNewString);
            }
        }
        /// <summary>
        /// 打开数据库
        /// </summary>
        public static void OpenDB()
        {
            if (conn == null)
            {
                GetConnection();
            }
            if (conn.State == ConnectionState.Closed) conn.Open();
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public static void CloseDB()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        /// <summary>
        /// 查询单个结果值
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string ExcuteQueryValue(string query)
        {
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn as MySqlConnection);
            DataTable dt = new DataTable();
            try
            {
                adp.Fill(dt);
            }
            catch (Exception e)
            {
                return null;
            }
            if (dt != null)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }

        /// <summary>
        /// 执行SELECT查询语句，并返回DataTable对象。
        /// </summary>
        /// <param name="strSQL">需要执行的sql语句</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ExcuteQuery(string strSQL)
        {
            MySqlDataAdapter adp = new MySqlDataAdapter(strSQL, conn as MySqlConnection);
            DataTable dt = new DataTable();
            try
            {
                adp.Fill(dt);
            }
            catch (Exception e)
            {
                return null;
            }
            return dt;
        }
    }
}
