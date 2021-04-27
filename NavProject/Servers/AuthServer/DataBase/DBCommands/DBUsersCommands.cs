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
    
    static class DBUsersCommands
    {
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

        static public int GetUser(string email, string password)
        {
            DBConnect db = DBConnect.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(1) { new MySqlParameter("@email", email) };
            
            int result = Convert.ToInt32(db.ExecuteReader($"Select `id` from `Auth`.`Users` where `login`=@email and `password`=@password", paramList)[0]/*first row*/[0]/*first field*/);
            return result;
        }

        static public bool IsUserExist(string email)
        {
            DBConnect db = DBConnect.GetInstance();
            List<MySqlParameter> paramList = new List<MySqlParameter>(1) { new MySqlParameter("@email", email) };
            
            bool result = db.ExecuteReader($"Select case when (Count(*) > 0) then 'true' else 'false' from `Auth`.`Users` where `login`=@email", paramList)[0][0] == "true";
            return result;
        }

    }
}