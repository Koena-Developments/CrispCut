using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CrispCut.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(
                _configuration["SendGrid:SenderEmail"],
                _configuration["SendGrid:SenderName"]
            );
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            
            // It's good practice to handle potential exceptions from the API call
            // For now, we'll let it bubble up, but you could add try-catch here.
            await client.SendEmailAsync(msg);
        }
    }
}