using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PointOfSales.DataCenter.Application.Constants;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Exceptions;
using PointOfSales.DataCenter.Application.Features.Account.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Application.Interfaces.Jobs;
using PointOfSales.DataCenter.Domain.Settings;
using PointOfSales.DataCenter.Infrastructure.Persistence.Extensions;
using PointOfSales.DataCenter.Infrastructure.Persistence.Helpers;
using PointOfSales.DataCenter.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
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
        private readonly JwtSecurityTokenSettings _jwt;
        private readonly IMailJob _mail; 

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSecurityTokenSettings> jwt, IMailJob mail)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_emailService = emailService;
            //_client = client.Value;
            _jwt = jwt.Value;
            _mail = mail;
        }
        public async Task<Result<string>> RegisterAsync(string userName, string password, string email, string role)
        {

            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var roleExists = Enum.GetNames(typeof(AuthorizationConstants.Roles)).Any(x => x.ToLower() == role.ToLower());
                    if (roleExists)
                    {
                        var validRole = Enum.GetValues(typeof(AuthorizationConstants.Roles)).Cast<AuthorizationConstants.Roles>().Where(x => x.ToString().ToLower() == role.ToLower()).FirstOrDefault();
                        await _userManager.AddToRoleAsync(user, validRole.ToString());
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, AuthorizationConstants.baseRole.ToString());
                    }
                    await _mail.SendMailAsync(user.Email, "Welcome!", $"Thanks for registering in our System as {user.UserName}!. Your Password is '{password}'.Happy Sales!");

                }
                return result.ToApplicationResult("", user.Id);
            }
            else
            {
                return Result<string>.Failure(new List<string> { $"Email {user.Email } is already registered." });
            }
            //if (result.Succeeded)
            //{
            //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    //var callbackUrl = $"{_client.Url}{_client.EmailConfirmationPath}?uid={user.Id}&code={System.Net.WebUtility.UrlEncode(code)}";

            //    //await _emailService.SendEmailConfirmationAsync(email, callbackUrl);

            //    return result.ToApplicationResult("", user.Id);

            //}         
        }
        public async Task<Result<LoginUserViewModel>> LoginAsync(string password, string email)
        {
            
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), email, nameof(email));
            }
            //if (user == null)
            //    return BadRequest(new string[] { "Invalid credentials." });

            var loginModel = new LoginUserViewModel()
            {
                HasVerifiedEmail = false
            };

            // Only allow login if email is confirmed
            if (!user.EmailConfirmed)
            {
                return Result<LoginUserViewModel>.Failure(new List<string> { $"Email not confirmed for user {user.Email}." });
            }

            //// Used as user lock
            //if (user.LockoutEnabled)
            //    return BadRequest(new string[] { "This account has been locked." });

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                loginModel.HasVerifiedEmail = true;

                if (user.TwoFactorEnabled)
                {
                    loginModel.TFAEnabled = true;
                    return Result<LoginUserViewModel>.Success("Two Factor Authentication Enabled!",loginModel);
                }
                else
                {
                    JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                    loginModel.TFAEnabled = false;
                    loginModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    return Result<LoginUserViewModel>.Success(loginModel);
                }
            }
            return Result<LoginUserViewModel>.Failure(new List<string> { $"Incorrect Credentials for user {user.Email}.",  });
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }
    }
}
