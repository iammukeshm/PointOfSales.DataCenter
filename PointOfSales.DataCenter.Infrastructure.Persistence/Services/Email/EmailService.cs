using CoreLibrary.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.Domain.Settings;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtp;
        private readonly ILogger _logger;
        public EmailService(IOptions<SmtpSettings> smtp, ILogger<EmailService> logger)
        {
            _logger = logger;
            _smtp = smtp.Value;
        }
        [DisplayName("Mailing {0}")]
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                if(_smtp.IsActive)
                {
                    using (EmailHelper emailHelper = new EmailHelper())
                    {
                        await emailHelper.SendMailAsync(_smtp.FromMailAddress, _smtp.DisplayName, toEmail, subject, body, _smtp.Port, _smtp.Host, _smtp.Password);
                        _logger.LogInformation("Mail Sent Successfully to {recipient}.", toEmail);
                    }                  
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Caught an Error in SendEmailAsync()");
            }
        }
    }
}
