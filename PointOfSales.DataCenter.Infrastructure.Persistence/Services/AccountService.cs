using Microsoft.AspNetCore.Identity;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Infrastructure.Persistence.Extensions;
using PointOfSales.DataCenter.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IEmailService _emailService;
        //private readonly ClientAppSettings _client;
        //private readonly JwtSecurityTokenSettings _jwt;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_emailService = emailService;
            //_client = client.Value;
            //_jwt = jwt.Value;
        }
        public async Task<Result<string>> RegisterAsync(string userName, string password, string email)
        {

            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
            };
            var result = await _userManager.CreateAsync(user, password);
            //if (result.Succeeded)
            //{
            //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    //var callbackUrl = $"{_client.Url}{_client.EmailConfirmationPath}?uid={user.Id}&code={System.Net.WebUtility.UrlEncode(code)}";

            //    //await _emailService.SendEmailConfirmationAsync(email, callbackUrl);

            //    return result.ToApplicationResult("", user.Id);
            //}
            return result.ToApplicationResult("", user.Id);
        }
    }
}
