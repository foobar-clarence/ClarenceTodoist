using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarenceTodoist
{
    public static class MysqlVisit
    {
        private static MySqlConnection conn;

        public static MySqlConnection GetMysqlConnection()
        {
            string connstr = "server=127.0.0.1;port=3306;user=root;password=CLarenGc5416;database=clatodoist";

            conn = new MySqlConnection(connstr);

            conn.Open();
            return conn;
        }

        public static MySqlCommand GetMySqlCommand(string mysqlStatement)
        {
            MySqlCommand cmd = new MySqlCommand(mysqlStatement, GetMysqlConnection());
            return cmd;
        }

        public static MySqlDataReader GetReader(string mysqlStatement)
        {
            var reader = GetMySqlCommand(mysqlStatement).ExecuteReader();
            return reader;
        }

        public static int ExNonQuery(string mysqlStatement) // return: row affected
        {
            return GetMySqlCommand(mysqlStatement).ExecuteNonQuery();
        }

        public static void CloseMysql()
        {
            conn.Close();
        }
    }
}
