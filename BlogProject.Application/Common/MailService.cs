using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Common
{
    public class MailService : IMailService
    {

        private IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = "ed0c78688f3709",
                    Password = "1fb290a61418a2"
                };

                client.Credentials = credentials;
                client.Host = "smtp.mailtrap.io";
                client.Port = 2525;
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(toEmail));
                    emailMessage.From = new MailAddress("your_email@example.com");
                    emailMessage.Subject = subject;
                    emailMessage.Body = content;
                    emailMessage.IsBodyHtml = true;

                    await client.SendMailAsync(emailMessage);
                }
            }


        }
    }

}