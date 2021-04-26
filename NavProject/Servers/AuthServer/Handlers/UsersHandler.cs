using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AuthServer.DataBase;

namespace AuthServer.Handlers
{
    public class UsersHandler
    {
        public void AddUser()
        {

        }

        public void UpdateUser()
        {

        }

        public bool SendConfirmLink(string email, string firstName, string link)
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
                $"<p> Здравствуйте, {firstName}.</p>\n" +
                "<p> Этот адрес электронной почты был указан для регистрации нового пользователя в самом странном проекте по созданию планов зданий.</p>\n" +
                "<p> Для завершения регистрации перейдите по ссылке, расположенной ниже:</p>\n" +
                $"<h3><strong>{link}</strong><strong></strong></h3>\n" +
                "<p> Учтите, что код действителен в течение 60 минут.</p>\n" +
                "<p></p>\n" +
                "<p> С уважением,</p>\n" +
                "<p> команда NavProject </p>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);
            smtpClient.Send(mailMessage);
            return true;
        }

        public bool ChangePassword(string email)
        {
            if (DBCommands.IsUserExist(email))
            {
                // send link
                return true;
            }
            return false;
        }
    }
}