using PointOfSales.DataDistribution.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataDistribution.Infrastructure.Persistence.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
