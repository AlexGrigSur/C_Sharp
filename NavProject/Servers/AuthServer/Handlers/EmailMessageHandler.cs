using System.Net;
using System.Net.Mail;

namespace AuthServer.Handlers
{
    public class EmailMessageHandler
    {
        // HIDE GOOGLE LOGIN/PASSWORD
        static private SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("navproject.kubsu@gmail.com", "146923785Alex"),
                EnableSsl = true
            };
        
        public static bool SendEmail(string subject, string body, string destination)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("navproject.kubsu@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(destination);
            smtpClient.Send(mailMessage);
            return true;
        }
    }
}