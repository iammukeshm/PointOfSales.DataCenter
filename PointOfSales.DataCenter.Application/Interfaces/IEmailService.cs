using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string ToEmail, string subject, string body);
    }
}
