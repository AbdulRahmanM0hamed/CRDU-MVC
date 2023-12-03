using MVC_DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVCS3PL.Helpers
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("Abdalrhmanmohammad437@gmail.com", "srqaunepyoyjhczv");
            Client.Send("Abdalrhmanmohammad437@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
