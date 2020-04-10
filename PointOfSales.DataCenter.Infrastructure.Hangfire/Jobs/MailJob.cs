using Hangfire;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Application.Interfaces.Jobs;
using System;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Hangfire.Jobs
{
    public class MailJob : IMailJob
    {
        private readonly IEmailService _emailSender;
        public MailJob(IEmailService emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task SendMailAsync(string email, string subject, string body)
        {
            BackgroundJob.Schedule(()=> _emailSender.SendEmailAsync(email, subject, body),new TimeSpan(0,0,10));
        }
    }
}
