using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Czech_Fitness
{
    static public class DB
    {
        static public MySqlConnection connection { get; } = new MySqlConnection($"server=localhost;port=3306;username=root;password=root");

        static private void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        static private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        static public void ExecuteCommand(string SQLcommand)
        {
            OpenConnection();
            using (MySqlCommand command = new MySqlCommand(SQLcommand, connection))
            {
                command.ExecuteNonQuery();
            }
            CloseConnection();
        }
        static public MySqlDataReader ExecuteReader(string SQLcommand)
        {
            OpenConnection();
            MySqlDataReader reader;
            using (MySqlCommand command = new MySqlCommand(SQLcommand, connection))
            {
                reader = command.ExecuteReader();
            }
            CloseConnection();
            return reader;
        }
    }
}
