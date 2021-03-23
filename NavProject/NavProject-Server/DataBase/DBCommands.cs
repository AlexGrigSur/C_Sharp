using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Server.Connect;

namespace NavProject_Server.DataBase
{
    static class DBCommands
    {
        static DBConnect db = DBConnect.GetInstance();
        static public void InitDB()
        {
            db.ExecuteCommand("create database if not exists `NavProject`");

            db.ExecuteCommand("create table if not exists `NavProject`.`Regions` (" + // REGIONS
                    "`id` int(11) not null primary key auto_increment," +
                    "`region_name` varchar(150) not null)");

            db.ExecuteCommand("create table if not exists `NavProject`.`Maps` (" + // MAPS
                     "`id` int(11) not null primary key auto_increment," +
                     "`building_name` varchar(150) not null," +
                     "`region_ID` int(11) not null, " +
                     "`building_address` varchar(150) not null, " +
                     "`isNavAble` boolean not null," +
                     "`isPublic` boolean not null, " +
                     "`buildingSerialized` longtext," +
                     "constraint `region_FK` foreign key(`region_ID`) references `NavProject`.`Regions`(`id`) on delete cascade)");

            db.ExecuteCommand("create table if not exists `NavProject`.`UsersCreations` (" + //  UserCreations
                    "`user_ID` int(11) not null," +
                    "`map_ID` int(11) not null," +
                    "constraint `PrimKey` primary key(`user_ID`,`map_ID`)," +
                    "constraint `User_FK` foreign key(`user_ID`) references `NavProject`.`Users`(`id`) on delete cascade," +
                    "constraint `Map_FK` foreign key(`map_ID`) references `NavProject`.`Maps`(`id`) on delete cascade)");
        }
    }
}

#region Помойка

//db.ExecuteCommand("create table if not exists `NavProject`.`Users` (" + // USERS
//        "`id` int(11) not null primary key auto_increment," +
//        "`name` varchar(150) not null," +
//        "`login` varchar(150) not null unique," +
//        "`password` varchar(150) not null," +
//        "`email` varchar(150) not null unique," +
//        "`isConfirmed` boolean not null)");

//static public List<string> InsertUser(string user_name, string login, string password, string email)
//{
//    db.ExecuteCommand($"Insert into `NavProject`.`Users` values(null,'{user_name}','{login}','{password}','{email}','0')");
//    return new List<string>();
//}
//static public List<string> GetUser(string login, string password)
//{
//    db.ExecuteReader($"Select * from `USERS` where `login`='{login}' and `password`='{password}'");
//    return new List<string>();
//}
//static public List<string> ConfirmUser(int id)
//{
//    db.ExecuteCommand($"Update `NavProject`.`Users` set `isConfirmed`='1' where `id`='{id}'");
//    return new List<string>();
//}
#endregion