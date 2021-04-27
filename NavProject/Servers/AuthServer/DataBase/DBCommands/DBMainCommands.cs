using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using AuthServer.DataBase.Connection;
using AuthServer.Models;

namespace AuthServer.DataBase.DBCommands
{
    public class DBMainCommands
    {
        /// <summary>
        /// Command that initialize dataBase
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        static public bool InitDB(params string[] values)
        {
            DBConnect db = DBConnect.GetInstance();
            try
            {
                db.ExecuteCommand("create database if not exists `Auth`");
                db.ExecuteCommand("create table if not exists `Auth`.`Users` " +
                    "( `id` int(11) not null primary key auto_increment," +
                    "`FirstName` varchar(150) not null," +
                    "`Email` varchar(150) not null unique," +
                    "`Password` varchar(150) not null");

                db.ExecuteCommand("create table if not exists `Auth`.`RefreshTokens` " +
                "( `user_id` int(11) not null primary key," +
                " `RefreshToken` varchar(150) not null," +
                " constraint `UsersFK` foreign key(`user_id`) references `Users`.`id` on delete cascade)");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}