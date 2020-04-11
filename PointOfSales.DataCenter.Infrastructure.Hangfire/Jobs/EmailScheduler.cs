using Hangfire;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Application.Interfaces.Jobs;
using System;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Hangfire.Jobs
{
    public class EmailScheduler : IEmailScheduler
    {
        private readonly IEmailService  _emailService;
        public EmailScheduler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public void Schedule(string email, string subject, string body)
        {
            BackgroundJob.Schedule(()=> _emailService.SendEmailAsync(email, subject, body),new TimeSpan(0,0,10));
        }
    }
}
