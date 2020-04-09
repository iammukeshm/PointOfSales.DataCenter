using PointOfSales.DataDistribution.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataDistribution.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Result<string>> RegisterAsync(string userName, string password, string email);
    }
}
