using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Features.Account.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Interfaces
{
    public interface IAccountService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<Result<string>> RegisterAsync(string userName, string password, string email);
        Task<Result<LoginUserViewModel>> LoginAsync(string password, string email);
    }
}
