using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Interfaces.Jobs
{
    public interface IMailJob
    {
        public void ScheduleMailInTenSeconds(string email, string subject, string body);
    }
}
