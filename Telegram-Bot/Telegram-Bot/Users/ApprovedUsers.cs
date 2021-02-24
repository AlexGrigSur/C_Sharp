using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_Bot.Users
{
    static class ApprovedUsers
    {
        static private List<long> Users = new List<long>();

        static Boolean CheckUser(long id) => Users.Contains(id);
        static void AddUser(long id)
        {
            if (Users.Contains(id)) return;
            Users.Add(id);
            DataBase.Add(id);
        }

        static void AddSubscriber(long id)
        {
            if (!Users.Contains(id)) return;
            
            DataBase.Add(id);
        }
    }
}
