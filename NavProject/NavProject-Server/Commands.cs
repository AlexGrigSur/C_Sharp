using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.Mail;
using System.Net;

namespace NavProject_Server
{
    class Commands
    {
        private static string CodeGenerator(int length)
        {
            Random rand = new Random();
            string result = "";
            int value;
            for (int i = 0; i < length; ++i)
            {
                value = rand.Next(0, 36);
                result += (value < 26) ? Convert.ToChar(value + 65).ToString() : (value - 26).ToString();
            }
            return result;
        }

        static public string InsertUser(ref string[] command, out User? user)
        {
            if (command.Count() == 5)
            {
                DBInteraction.InsertUser(command[1], command[2], command[3], command[4]);
                user = DBInteraction.GetUser(command[2], command[3]);
                return "Регистрация успешно пройдена";
            }
            else
            {
                user = null;
                return "Ошибка с количеством данных запроса";
            }
        }
        static public string Authorize(out User? user, ref string[] command)
        {
            if (command.Count() == 3)
            {
                user = DBInteraction.GetUser(command[1], command[2]);
                return ((user == null) ? "No matches found" : $"Welcome back,{user.Value.name}");
            }
            else
            {
                user = null;
                return "Ошибка с количеством данных запроса";
            }
        }
        static public string SendConfirmEmailCode(ref User? user, out string currentMode, out string buffer, ref DateTime lastBufferRequestTime)
        {
            currentMode = "";
            buffer = "";

            if (user == null)
                return "Authorization required";
            if (user.Value.isConfirmed)
                return "Email is already confirmed";

            currentMode = "ConfirmEmail";
            lastBufferRequestTime = DateTime.UtcNow;
            buffer = CodeGenerator(6);

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
                $"<p> Здравствуйте, {user.Value.name}.</p>\n" +
                "<p> Этот адрес электронной почты был указан для регистрации нового пользователя в самом странном проекте по созданию планов зданий.</p>\n" +
                "<p> Ваш код регистрации:</p>\n" +
                $"<h3><strong>{buffer}</strong><strong></strong></h3>\n" +
                "<p> Учтите, что код действителен в течение 60 минут.</p>\n" +
                "<p></p>\n" +
                "<p> С уважением,</p>\n" +
                "<p> команда NavProject </p>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(user.Value.email);
            smtpClient.Send(mailMessage);
            return "Message was sent";
        }
        static public string ConfirmEmail(ref User? user, ref string[] command, ref string currentMode, ref string buffer, ref DateTime lastBufferRequestTime)
        {
            if (command.Count() != 2)
                return "Ошибка с количеством данных запроса";
            if (user == null)
                return "Необходим вход в систему для подтверждения аккаунта";
            if (currentMode != "ConfirmEmail")
                return "Нет текущего кода для подтверждения аккаунта";
            if (command[1] == buffer && DateTime.Now <= lastBufferRequestTime.AddHours(1))
            {
                user = new User(user.Value.id, user.Value.name, user.Value.login, user.Value.password, user.Value.email, true);
                DBInteraction.ConfirmUser(user.Value.id);
                return "Аккаунт успешно подтверждён.";
            }
            else
                return "Неверный код или истекло время его использования";
        }
        static public string ForgotPassword()
        {
            return "";
        }
        static public void GetPlansList_Public()
        {

        }
        static public void Disconnect(int id, ref NetworkStream stream, ref TcpClient client)
        {
            Console.WriteLine($"[{id}]Client disconnected");
            stream.Close();
            client.Close();
        }
        static public string InsertLotOfUsers()
        {
            DBInteraction.InsertLotRows(400);
            return "Done";
        }
    }
}
