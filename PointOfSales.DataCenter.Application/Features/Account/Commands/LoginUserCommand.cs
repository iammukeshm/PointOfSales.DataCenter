using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Features.Account.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.Account.Commands
{
    public class LoginUserCommand : IRequest<Result<LoginUserViewModel>>
    {

        public string Email { get; set; }

        public string Password { get; set; }
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginUserViewModel>>
        {
            private readonly IAccountService _accountService;
            public LoginUserCommandHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<Result<LoginUserViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                return await _accountService.LoginAsync( request.Password, request.Email);

               //return new Result<LoginUserViewModel>(true, new string[] { string.Format("Successfuly logged in as {0}", request.UserName) }, result);
            }
        }
    }
}
