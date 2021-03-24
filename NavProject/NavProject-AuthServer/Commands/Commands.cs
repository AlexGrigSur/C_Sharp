using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_AuthServer.Commands
{
    interface ICommand
    {
        public void RunCommand(params string[] values);
    }
    class SendConfirmEmailLink:ICommand
    {
        public void RunCommand(params string[] values)
        {

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("navproject.kubsu@gmail.com", "146923785Alex"),
                EnableSsl = true
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress("navproject.kubsu@gmail.com"),
                Subject = "NavProject - регистрация нового пользователя",
                Body = "<p></p>\n" +
                "<h2> NavProject</h2>\n" +
                "<p></p>\n" +
                //$"<p> Здравствуйте, {user.Value.name}.</p>\n" +
                "<p> Этот адрес электронной почты был указан для регистрации нового пользователя в самом странном проекте по созданию планов зданий.</p>\n" +
                "<p> Для завершения регистрации перейдите по ссылке, расположенной ниже:</p>\n" +
                $"<h3><strong>{""}</strong><strong></strong></h3>\n" +
                "<p> Учтите, что код действителен в течение 60 минут.</p>\n" +
                "<p></p>\n" +
                "<p> С уважением,</p>\n" +
                "<p> команда NavProject </p>",
                IsBodyHtml = true
            };
            mailMessage.To.Add("");//user.Value.email);
            smtpClient.Send(mailMessage);
            //return "Message was sent";
        }
    }
    class ConfirmEmail : ICommand
    {
        public void RunCommand(params string[] values)
        {
            // Decrypt User Data
            // if time valid - insert User in DB
        }
    }
    class Authorize : ICommand
    {
        public void RunCommand(params string[] values)
        {
            // CheckUserInDB
            // Create Token
            // Create Refresh
            // SendToUser
        }
    }
    class ForgotPassword : ICommand
    {
        public void RunCommand(params string[] values)
        {
            // CheckUserEmailInDB
            // Create RefreshLink
            // SendByEmail
            // SendToUser
        }
    }
}
#region Obsolette
//private static string CodeGenerator(int length)
//{
//    Random rand = new Random();
//    string result = "";
//    int value;
//    for (int i = 0; i < length; ++i)
//    {
//        value = rand.Next(0, 36);
//        result += (value < 26) ? Convert.ToChar(value + 65).ToString() : (value - 26).ToString();
//    }
//    return result;
//}
#endregion