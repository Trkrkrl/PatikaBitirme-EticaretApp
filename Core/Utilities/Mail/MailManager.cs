using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Mail
{
    public class MailManager : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            emailMessage.FromAddresses.Add(new EmailAddress()//appsettingseten gönderici olan admin mailimizi alır ve gönderenler listesine ekler
            {
                Address = _configuration.GetSection("EmailConfiguration").GetSection("Mail").Value,
                Name = _configuration.GetSection("EmailConfiguration").GetSection("DisplayName").Value
            });
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));//bunu yukardan getirir

            message.Subject = emailMessage.Subject;


            var messageBody = string.Format(emailMessage.Subject, emailMessage.Content);

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = messageBody
            };
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(
                    _configuration.GetSection("EmailConfiguration").GetSection("Host").Value,
                    Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("Port").Value),
                    SecureSocketOptions.StartTls
                    );
                emailClient.Authenticate(
                    _configuration.GetSection("EmailConfiguration").GetSection("Mail").Value,
                    _configuration.GetSection("EmailConfiguration").GetSection("Password").Value
                    );
               
                await emailClient.SendAsync(message);
                emailClient.Disconnect(true);
            }
        }
    }
}
