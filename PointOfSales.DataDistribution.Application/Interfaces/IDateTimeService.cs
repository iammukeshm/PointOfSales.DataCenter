using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataDistribution.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
