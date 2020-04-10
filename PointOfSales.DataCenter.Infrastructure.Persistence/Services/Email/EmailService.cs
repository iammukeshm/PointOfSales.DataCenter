using Microsoft.Extensions.Options;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.Domain.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtp;
        public EmailService(IOptions<SmtpSettings> smtp)
        {
            _smtp = smtp.Value;
        }
        [DisplayName("Mailing {0}")]
        public Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                if(_smtp.IsActive)
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(_smtp.FromMailAddress, _smtp.DisplayName);
                    message.To.Add(new MailAddress(toEmail));
                    message.Subject = subject;
                    message.IsBodyHtml = false;  
                    message.Body = body;
                    smtp.Port = _smtp.Port;
                    smtp.Host = _smtp.Host;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_smtp.FromMailAddress, _smtp.Password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }
                
            }
            catch (Exception ex)
            {

            }
            
            // TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
            return Task.CompletedTask;
        }
    }
}
