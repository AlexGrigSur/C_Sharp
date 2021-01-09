using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_Server
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
            connection = new MySqlConnection($"server=localhost;port=3306;username=root;password=root;database={DBName}");
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
                //MessageBox.Show("Программа не может подключиться к базе данных. Завершение работы");
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
            using (MySqlCommand command = new MySqlCommand(SQLcommand, GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }
        public MySqlDataReader ExecuteReader(string SQLcommand)
        {
            MySqlDataReader reader;
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

    public class DBInitialize
    {
        static public void InitDB()
        {
            using (DB DataBase = new DB())
            {
                //DataBase.ExecuteCommand("drop database `NavProject`");
                DataBase.ExecuteCommand("create database if not exists `NavProject`");
                DataBase.NewConnection("NavProject");
                DataBase.ExecuteCommand("create table if not exists `Users` (" + // USERS
                    "`id` int(11) not null primary key auto_increment," +
                    "`name` varchar(150) not null," +
                    "`login` varchar(150) not null unique," +
                    "`password` varchar(150) not null," +
                    "`email` varchar(150) not null unique," +
                    "`isConfirmed` boolean not null)"); // add lastOnline datetime

                DataBase.ExecuteCommand("create table if not exists `Regions` (" + // REGIONS
                    "`id` int(11) not null primary key auto_increment," +
                    "`region_name` varchar(150) not null)");

                DataBase.ExecuteCommand("create table if not exists `Maps` (" + // MAPS
                     "`id` int(11) not null primary key auto_increment," +
                     "`building_name` varchar(150) not null," +
                     "`region_ID` int(11) not null, " +
                     "`building_address` varchar(150) not null, " +
                     "`isNavAble` boolean not null," +
                     "`isPublic` boolean not null, " +
                     "`buildingSerialized` longtext," +
                     "constraint `region_FK` foreign key(`region_ID`) references `Regions`(`id`) on delete cascade)");

                DataBase.ExecuteCommand("create table if not exists `UsersCreations` (" + //  UserCreations
                    "`user_ID` int(11) not null," +
                    "`map_ID` int(11) not null," +
                    "constraint `PrimKey` primary key(`user_ID`,`map_ID`)," +
                    "constraint `User_FK` foreign key(`user_ID`) references `Users`(`id`) on delete cascade," +
                    "constraint `Map_FK` foreign key(`map_ID`) references `Maps`(`id`) on delete cascade)");

                using (MySqlDataReader reader = DataBase.ExecuteReader("select count(*) from `Regions`"))
                {
                    if (reader.Read())
                        if (reader.GetInt32(0) == 0)
                            InsertBaseRegions();
                }
            }
        }
        static public void InsertBaseRegions()
        {
            using (DB DataBase = new DB("NavProject"))
            {
                DataBase.ExecuteCommand("Insert into `Regions` values (null,'Краснодар'),(null,'Анапа'),(null,'Ставрополь')");
            }
        }
    }

    class DBInteraction
    {
        static public void InsertUser(string user_name, string login, string password, string email)
        {
            using (DB DataBase = new DB("NavProject"))
            {
                DataBase.ExecuteCommand($"Insert into `Users` values(null,'{user_name}','{login}','{password}','{email}','0')");
            }
        }
        static public User? GetUser(string login, string password)
        {
            User? user = null;
            using (DB DataBase = new DB("NavProject"))
            {
                using (MySqlDataReader reader = DataBase.ExecuteReader($"Select * from `USERS` where `login`='{login}' and `password`='{password}'"))
                {
                    if (reader.Read())
                        user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetBoolean(5));
                }
            }
            return user;
        }

        static public void ConfirmUser(int id)
        {
            using (DB DataBase = new DB("NavProject"))
            {
                DataBase.ExecuteCommand($"Update `Users` set `isConfirmed`='1' where `id`='{id}'");
            }
        }
        static public void InsertLotRows(int count)
        {
            using (DB DataBase = new DB("NavProject"))
            {
                for (int i = 0; i < count; ++i)
                    DataBase.ExecuteCommand($"Insert into `Maps` values(null,'building_{i}','1','кыкистан','1','1','KubanStateUniversity.map')");
            }
        }
    }

}
