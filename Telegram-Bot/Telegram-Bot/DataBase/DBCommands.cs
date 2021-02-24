using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Czech_Fitness
{
    static public class DBCommands
    {
        static public void CreateDataBase(/*ref DB database*/)
        {
            DB.ExecuteCommand("Create database if not exists `TelegramBot`");

            DB.ExecuteCommand("Create table if not exists `TelegramBot`.`Users` (" +
                "`id` int(11) not null primary key auto_increment," +
                "`telegram_id` int(11) not null unique)");

            DB.ExecuteCommand("Create table if not exists `TelegramBot`.`Subscriptions` (" +
                "`User_ID` int(11) not null," +
                "`isSubscribed` boolean not null," +
                "constraint `User_FK` primary key(`User_ID`) references `Users`(`id`) on delete cascade)");
        }

        static List<long> GetUsers()
        {
            List<long> users = null;

            MySqlDataReader reader = DB.ExecuteReader($"Select `telegram_id` from `Users`");

            users = new List<long>(reader.RecordsAffected);
            while (reader.Read())
                users.Add(reader.GetInt64(0));

            return users;
        }
        static Dictionary<long, bool> GetSubscriptions()
        {
            MySqlDataReader reader = DB.ExecuteReader($"Select `telegram_id`,`isSubscribed` from `TelegramBot`.`Users` as `Us` inner join `TelegramBot`.`Subscriptions` as `Sub` on `Us`.`telegram_id`=`Sub`.`User_ID`");
            
            Dictionary<long, bool> subscriptions = new Dictionary<long, bool>(reader.RecordsAffected);
           
            while (reader.Read())
                subscriptions.Add(reader.GetInt64(0), reader.GetBoolean(1));
            
            return subscriptions;
        }
        static public void InsertUser(long id)
        {
            DB.ExecuteCommand($"Insert into `TelegramBot`.`Users` values(null,'{id}')");
        }
        //static public void UnSubscribe(long id)
        //{
        //    DB.ExecuteCommand($"Insert into `TelegramBot`.`Users` values(null,'{id}')");
        //}
    }
}
