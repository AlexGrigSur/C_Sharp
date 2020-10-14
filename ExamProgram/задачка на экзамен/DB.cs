using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace DataBase
{
    abstract class DB
    {
        protected MySqlConnection connection;

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }

    class DBInteractive: DB
    {
        public DBInteractive()
        {
            connection = new MySqlConnection("server=localhost;port=3306;username=root;password=;");
        }
        public DBInteractive(string DBName)
        {
            connection = new MySqlConnection($"server=localhost;port=3306;username=root;password=;database={DBName}");
        }
        public void ExecuteCommand(string cmd)
        {
            using (MySqlCommand command = new MySqlCommand(cmd,GetConnection()))
            {
                openConnection();
                command.ExecuteNonQuery();
                closeConnection();
            }
        }

        public MySqlDataReader ExecuteReader(string cmd)
        {
            using (MySqlCommand command = new MySqlCommand(cmd,GetConnection()))
            {
                openConnection();
                MySqlDataReader reader = command.ExecuteReader();
                return reader;
            }
        }

        public DataView DGFill(string cmd)
        {
            DataTable table = new DataTable();
            openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(cmd, GetConnection());
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }

            closeConnection();
            return table.DefaultView;
        }
    }
}
