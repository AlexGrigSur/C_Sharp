using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavTestNoteBookNeConsolb
{
    class DB
    {
        private MySqlConnection connection;
        public DB()
        {
            try
            {
                connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;Convert Zero Datetime=True");
            }
            catch
            {
                System.Environment.Exit(1);
            }
        }
        public DB(string DBName)
        {
            try
            {
                connection = new MySqlConnection($"server=localhost;port=3306;username=root;password=root;database={DBName}");//Convert Zero Datetime=True");
            }
            catch
            {
                System.Environment.Exit(1);
            }
        }
        #region // connection
        private void OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Программа не может подключиться к базе данных. Завершение работы");
                System.Environment.Exit(1);
            }
        }
        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        private MySqlConnection GetConnection()
        {
            return connection;
        }
        #endregion
        public void ExecuteCommand(string SQLcommand)
        {
            OpenConnection();
            using (MySqlCommand command = new MySqlCommand(SQLcommand, GetConnection()))
            {
                command.ExecuteNonQuery();
            }
            CloseConnection();
        }
        public MySqlDataReader ExecuteReader(string SQLcommand)
        {
            MySqlDataReader reader;
            OpenConnection();
            using (MySqlCommand command = new MySqlCommand(SQLcommand, GetConnection()))
            {
                reader = command.ExecuteReader();
            }
            return reader;
        }
    }
}
