using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Interfaces.Jobs
{
    public interface IEmailScheduler
    {
        public void Schedule(string email, string subject, string body);
    }
}
