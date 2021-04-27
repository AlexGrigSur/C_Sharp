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
            string subject = "NavProject - регистрация нового пользователя";

            string body = "<p></p>\n" +
            "<h2> NavProject</h2>\n" +
            "<p></p>\n" +
            $"<p> Здравствуйте, {firstName}.</p>\n" +
            "<p> Этот адрес электронной почты был указан для регистрации нового пользователя в самом странном проекте по созданию планов зданий.</p>\n" +
            "<p> Для завершения регистрации перейдите по ссылке, расположенной ниже:</p>\n" +
            $"<h3><strong>{link}</strong><strong></strong></h3>\n" +
            "<p> Учтите, что код действителен в течение 60 минут.</p>\n" +
            "<p></p>\n" +
            "<p> С уважением,</p>\n" +
            "<p> команда NavProject </p>";

            return EmailMessageHandler.SendEmail(subject,body, email);
        }

        public bool SendChangePasswordLink(string email, string firstName, string link)
        {
            string subject = "NavProject - сброс пароля";

            string body = "<p></p>\n" +
            "<h2> NavProject</h2>\n" +
            "<p></p>\n" +
            $"<p> Здравствуйте, {firstName}.</p>\n" +
            "<p> С вашего аккаунта был отправлен запрос на смену пароля</p>\n" +
            "<p> Для смены пароля вам требуется скопировать данную ссылку в в окно смены пароля:</p>\n" +
            $"<h3><strong>{link}</strong><strong></strong></h3>\n" +
            "<p> Учтите, что код действителен в течение 60 минут.</p>\n" +
            "<p></p>\n" +
            "<p> С уважением,</p>\n" +
            "<p> команда NavProject </p>";

            return EmailMessageHandler.SendEmail(subject,body, email);
        }

        public bool ChangePassword(string email)
        {
            if (DBUsersCommands.IsUserExist(email))
            {
                return true;
            }
            return false;
        }
    }
}