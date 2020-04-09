using PointOfSales.DataCenter.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
