using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;

namespace CarService.Class
{
    public class DBConnection
    {
        static string port = "3307";

        static MySqlConnection conn;
        static public MySqlCommand command;
        static public MySqlDataAdapter adapter;
        static public MySqlDataReader reader;

        static public DataTable dtClients = new DataTable();
        static public DataTable dtGender = new DataTable();
        static public DataTable dtTag = new DataTable();

        /// <summary>
        /// Метод подключения к базе данных
        /// </summary>
        /// <returns>Логическое</returns>
        public static bool ConnectDb()
        {
            try
            {
                string connectionString = $@"Database = auto_service_task_4; data source = localhost; port = {port}; user = root; password = qwerty; charset = utf8mb4";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                command = new MySqlCommand();
                command.Connection = conn;
                adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }
        /// <summary>
        /// Метод отключения от базы данных
        /// </summary>
        /// <returns>Логическое</returns>
        public static bool CloseDb()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
