using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Database
{
    static public class DBCommands
    {
        static public void CreateDataBase(/*ref DB database*/)
        {
            DB.GetInstance().ExecuteCommand("Create database if not exists `TelegramBot`");

            DB.GetInstance().ExecuteCommand("Create table if not exists `TelegramBot`.`Users` (" +
                "`id` int(11) not null primary key auto_increment," +
                "`telegram_id` int(11) not null unique)");

            DB.GetInstance().ExecuteCommand("Create table if not exists `TelegramBot`.`Subscriptions` (" +
                "`User_ID` int(11) not null," +
                "`isSubscribed` boolean not null," +
                "constraint `User_FK` primary key(`User_ID`) references `Users`(`id`) on delete cascade)");
        }

        static List<long> GetUsers()
        {
            List<long> users = null;

            DB.GetInstance().ExecuteReader("Select `telegram_id` from `Users`");


            return users;
        }
        static Dictionary<long, bool> GetSubscriptions()
        {
            List<string> result = DB.GetInstance().ExecuteReader($"Select `telegram_id`,`isSubscribed` from `TelegramBot`.`Users` as `Us` inner join `TelegramBot`.`Subscriptions` as `Sub` on `Us`.`telegram_id`=`Sub`.`User_ID`");
            
            Dictionary<long, bool> subscriptions = new Dictionary<long, bool>(result.Count);

            return subscriptions;
        }
        static public void InsertUser(long id)
        {
            string sql = "Insert into `TelegramBot`.`Users` values(null,@user)";
            MySqlParameter param = new MySqlParameter();
            DB.GetInstance().ExecuteCommand();
        }
        static public void Unsubscribe(long id)
        {
            string sql = "Insert into `TelegramBot`.`Users` values(null,@UserID)";
            MySqlParameter param = new MySqlParameter("@UserID", "");
            DB.GetInstance().ExecuteCommand(sql,new List<MySqlParameter> { param });
            
        }
    }
}
