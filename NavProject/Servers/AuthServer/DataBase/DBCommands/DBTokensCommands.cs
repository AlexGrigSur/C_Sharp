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
    static class DBTokensCommands
    {
        static public bool GetRefreshToken(int userID, string refreshToken)
        {
            DBConnect db = DBConnect.GetInstance();
            
            List<MySqlParameter> paramList = new List<MySqlParameter>(2) { new MySqlParameter("@id", userID), new MySqlParameter("@refreshToken", refreshToken)};

            bool matchResult = (db.ExecuteReader($"Select case when (`RefreshToken` = @refreshToken) then 'true' else 'false' from `Auth`.`Tokens` where `user_id`=@id", paramList)[0][0] == "true");
            return matchResult;
        }

        static public bool UpdateTokens(int userID, string refreshToken)
        {
            DBConnect db = DBConnect.GetInstance();
            
            List<MySqlParameter> paramList = new List<MySqlParameter>(2) { new MySqlParameter("@id", userID),  new MySqlParameter("@refreshToken", refreshToken)};

            try
            {
                db.ExecuteCommand($"Insert into table `Auth`.`Tokens` values(@id, @refreshToken) on duplicate key update `RefreshToken`=@refreshToken", paramList);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}