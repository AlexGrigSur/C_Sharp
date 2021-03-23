using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.Mail;
using System.Net;
using NavProject_Server.DataBase;

namespace NavProject_Server
{
    interface ICommand
    {
        public List<string> RunCommand(params string[] values);
    }
    class CheckToken: ICommand
    {
        public List<string> RunCommand(params string[] command)
        {
            return null;
        }
    }
    class GetPublicPlans: ICommand
    {
        public List<string> RunCommand(params string[] command)
        {
            return null;
        }
    }
}