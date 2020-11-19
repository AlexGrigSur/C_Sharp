using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavTestNoteBookNeConsolb
{
    class DB : IDisposable
    {
        private MySqlConnection connection;
        public DB()
        {
            connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root");
            OpenConnection();
        }
        public DB(string DBName)
        {
            NewConnection(DBName);
        }

        public void NewConnection(string DBName)
        {
            //CloseConnection();
            connection = new MySqlConnection($"server=localhost;port=3306;username=root;password=root;database={DBName}");//Convert Zero Datetime=True");
            OpenConnection();
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
            //OpenConnection();
            using (MySqlCommand command = new MySqlCommand(SQLcommand, GetConnection()))
            {
                command.ExecuteNonQuery();
            }
            //CloseConnection();
        }
        public MySqlDataReader ExecuteReader(string SQLcommand)
        {
            MySqlDataReader reader;
            //OpenConnection();
            using (MySqlCommand command = new MySqlCommand(SQLcommand, GetConnection()))
            {
                reader = command.ExecuteReader();
            }
            return reader;
        }

        public void Dispose()
        {
            CloseConnection();
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
