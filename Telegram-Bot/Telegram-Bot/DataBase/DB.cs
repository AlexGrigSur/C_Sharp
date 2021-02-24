using System.Collections.Generic;
using MySqlConnector;

namespace Database
{
    public class DB
    {
        private MySqlConnection connection;
        private MySqlCommand command = new MySqlCommand();
        static protected DB instance = null;
        static private string connectString= "server=localhost;port=3306;username=root;password=root";
        private DB() =>
            connection = new MySqlConnection(connectString);

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
        private string GetJson(ref MySqlDataReader reader)
        {
            return "";
        }


        public static DB GetInstance()
        {
            if (instance == null)
            {
                instance = new DB();
                return instance;
            }
            else
                return instance;
        }
        public void ExecuteCommand(string SQLcommand, List<MySqlParameter> paramsColl=null)
        {
            command.CommandText = SQLcommand;
            command.Parameters.Clear();

            if(paramsColl!=null)
                foreach (MySqlParameter i in paramsColl)
                    command.Parameters.Add(i);

            OpenConnection();
            command.ExecuteNonQuery();
            CloseConnection();
        }
        public List<string> ExecuteReader(string SQLcommand, List<MySqlParameter> paramsColl=null)
        {
            command.CommandText = SQLcommand;
            command.Parameters.Clear();

            if (paramsColl != null)
                foreach (MySqlParameter i in paramsColl)
                    command.Parameters.Add(i);

            OpenConnection();
            
            MySqlDataReader reader = command.ExecuteReader();
            List<string> jsonStrings = new List<string>(reader.RecordsAffected);

            while (reader.Read())
                jsonStrings.Add(GetJson(ref reader));

            reader.Close();

            CloseConnection();
            
            return jsonStrings;
        }
    }
}
