using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace VideoShare.PL.App_Start
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var from = ConfigurationManager.AppSettings["EmailAddress"];
            var pass = ConfigurationManager.AppSettings["Password"];
            string host = ConfigurationManager.AppSettings["SMTPhost"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

            SmtpClient client = new SmtpClient(host, port);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            var mail = new MailMessage(from, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }
    }
}