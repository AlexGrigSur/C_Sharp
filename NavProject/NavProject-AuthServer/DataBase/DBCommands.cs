using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using NavProject_AuthServer.DataBase;

namespace NavProject_AuthServer.DataBaseCommands
{
    interface IDataBaseCommand
    {
        public List<string> RunCommand(params string[] values);
    }
    class InitDB : IDataBaseCommand
    {
        /// <summary>
        /// Command that initialize dataBase
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<string> RunCommand(params string[] values)
        {
            DBConnect db = DBConnect.GetInstance();
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
            return null;
        }
    }
    class InsertUser : IDataBaseCommand
    {
        /// <summary>
        /// Command that insert new user in DB
        /// </summary>
        /// <param name="values">params that contains userName, email and password</param>
        /// <returns>null</returns>
        public List<string> RunCommand(params string[] values)//string userName, string email, string password
        {
            if (values.Count() != 2)
                throw new ArgumentException("In this function `values` param should contain three elements: username, email and password");

            DBConnect db = DBConnect.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(3) { new MySqlParameter("@userName", values[0]), new MySqlParameter("@email", values[1]), new MySqlParameter("@pass", values[2]) };
            db.ExecuteCommand($"Insert into `Auth`.`Users` values(null,@userName,@email,@pass)", paramList);
            return null;
        }
    }
    class GetUser : IDataBaseCommand
    {
        /// <summary>
        /// Get User info from DataBase
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">params should contains two elements</exception>
        public List<string> RunCommand(params string[] values)//(string login, string password)
        {
            if (values.Count() != 2) throw new ArgumentException("In this function `values` param should contain two elements: email and password");

            DBConnect db = DBConnect.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(2) { new MySqlParameter("@email", values[0]), new MySqlParameter("@password", values[1]) };
            db.ExecuteReader($"Select * from `Auth`.`Users` where `login`=@email and `password`=@password");
            return new List<string>();
        }
    }
}