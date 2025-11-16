using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task SendEmailAsync(EmailDTO emailDTO)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["EmailSettings:From"], _configuration["EmailSettings:From"]));
            message.Subject = emailDTO.Subject;
            message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDTO.Content
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(
                   _configuration["EmailSettings:Smtp"],
                   int.Parse(_configuration["EmailSettings:Port"]!), true);
                    await client.AuthenticateAsync(_configuration["EmailSettings:From"],
                        _configuration["EmailSettings:Password"]);
                    await client.SendAsync(message);
                }
                catch 
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
               
                
            }
        }
    }
}
