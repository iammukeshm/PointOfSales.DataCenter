using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Interfaces.Jobs
{
    public interface IMailJob
    {
        public Task SendMailAsync(string email, string subject, string body);
    }
}
