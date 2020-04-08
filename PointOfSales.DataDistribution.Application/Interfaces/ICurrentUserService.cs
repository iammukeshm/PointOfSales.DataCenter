using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataDistribution.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
