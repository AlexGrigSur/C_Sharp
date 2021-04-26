using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using AuthServer.DataBase.Connection;
using AuthServer.Models;

namespace AuthServer.DataBase
{
    static class DBCommands
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

        // Rewrite summary for functions

        /// <summary>
        /// Command that insert new user in DB
        /// </summary>
        /// <param name="values">params that contains userName, email and password</param>
        /// <returns>true if success, else - false</returns>
        static public bool InsertUser(string firstName, string email, string password)//string userName, string email, string password
        {
            DBConnect db = DBConnect.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(3) { new MySqlParameter("@firstName", firstName), new MySqlParameter("@email", email), new MySqlParameter("@pass", password) };
            
            try
            {
                db.ExecuteCommand($"Insert into `Auth`.`Users` values(null,@firstName,@email,@pass)", paramList);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Rewrite summary for functions

        /// <summary>
        /// Get User info from DataBase
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">params should contains two elements</exception>
        static public User GetUser(string email)//(string login, string password)
        {
            DBConnect db = DBConnect.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(1) { new MySqlParameter("@email", email) };

            string[] result = db.ExecuteReader($"Select * from `Auth`.`Users` where `login`=@email and `password`=@password", paramList)[0];
            // IF NOT NULL/EMPTY
            return User.GetUser(Convert.ToInt32(result[0]), result[1], result[2], result[3]);
        }

        // Rewrite summary for functions

        static public User GetUser(string email, string password)//(string login, string password)
        {
            DBConnect db = DBConnect.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(1) { new MySqlParameter("@email", email), new MySqlParameter("@password", password) };

            string[] result = db.ExecuteReader($"Select * from `Auth`.`Users` where `login`=@email and `password`=@password", paramList)[0];
            // IF NOT NULL/EMPTY
            return User.GetUser(Convert.ToInt32(result[0]),result[1],result[2],result[3]);
        }


        // Rewrite summary for functions

        /// <summary>
        /// Get User info from DataBase
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">params should contains two elements</exception>
        static public bool IsUserExist(string email)//(string login, string password)
        {
            DBConnect db = DBConnect.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(1) { new MySqlParameter("@email", email) };
            
            bool result = (db.ExecuteReader($"Select Count(*) from `Auth`.`Users` where `login`=@email and `password`=@password", paramList)[0]/*first row*/[0]/*first field*/ == "1");
            return result;
        }

    }
}