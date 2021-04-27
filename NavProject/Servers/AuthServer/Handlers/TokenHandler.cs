using System;
using System.Text.Json;
using AuthServer.Models;
using AuthServer.DataBase;
using System.Security.Cryptography;
using System.Text;

namespace AuthServer.Handlers
{
    static class Tokens
    {
        static private string SECRET_KEY = "Test";

        ///summary//
        ///Get users id///
        static private int GetUserID(User user)
        {
            int userID = DBUsersCommands.GetUser(user.email, user.password);
            return (userID >= 0)
                ? userID
                : -1;
        }
            
        static private (string,string) GenerateTokens(int userID)
        {
            string Json = JsonSerializer.Serialize(new 
            {
                userID = userID,
                DateTimeCreated = DateTime.Now,
                expiredAt = 30
            });

            string accessToken;

            using (var hmacsha256 = new HMACSHA256(Encoding.Unicode.GetBytes(SECRET_KEY)))
            {
                var hash = hmacsha256.ComputeHash(Encoding.Unicode.GetBytes(Json));
                accessToken = Convert.ToBase64String(hash);
            }
            string refreshToken = Guid.NewGuid().ToString();

            return (accessToken,refreshToken);
        }

        static public (string, string) CreateTokens(User user)
        {
            int userID = GetUserID(user);
            if(userID != -1)
            {
                (string, string) tokens =  GetTokens(userID);
                return tokens;
            }
            return (null,null);
        }

        static private (string, string) GetTokens(int userID) 
        {
            (string, string) tokens = GenerateTokens(userID);
            DBTokensCommands.UpdateTokens(userID, tokens.Item1);
            return tokens;
        }

        static public (string, string) RefreshTokens(int userID, string refreshToken)
        {
            if(DBTokensCommands.GetRefreshToken(userID, refreshToken))
            {
                (string, string) tokens =  GetTokens(userID);
                return tokens;
            }
            return (null,null);
        }

    }
}