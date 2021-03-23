using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_Server.Connect
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private MySqlCommand command = new MySqlCommand();
        static protected DBConnect instance = null;
        static private string connectString = "server=localhost;port=3306;username=root;password=root;Allow User Variables=True";
        private DBConnect()
        {
            connection = new MySqlConnection(connectString);
            command = new MySqlCommand();
        }
        private void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public static DBConnect GetInstance()
        {
            if (instance == null)
            {
                instance = new DBConnect();
                return instance;
            }
            else
                return instance;
        }
        public void ExecuteCommand(string SQLcommand, List<MySqlParameter> paramsColl = null)
        {
            command.CommandText = SQLcommand;
            command.Connection = connection;
            command.Parameters.Clear();
            if (paramsColl != null)
                foreach (MySqlParameter i in paramsColl)
                    command.Parameters.Add(i);

            OpenConnection();
            command.ExecuteNonQuery();
            CloseConnection();
        }
        public List<string[]> ExecuteReader(string SQLcommand, List<MySqlParameter> paramsColl = null)
        {
            command.CommandText = SQLcommand;
            command.Connection = connection;
            command.Parameters.Clear();

            if (paramsColl != null)
                foreach (MySqlParameter i in paramsColl)
                    command.Parameters.Add(i);


            OpenConnection();

            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> result = new List<string[]>();

            while (reader.Read())
            {
                result.Add(new string[reader.FieldCount]);
                for (int j = 0; j < reader.FieldCount; ++j)
                    result.Last()[j] = reader[j].ToString();
            }
            reader.Close();
            CloseConnection();

            return result;
        }
    }
}
