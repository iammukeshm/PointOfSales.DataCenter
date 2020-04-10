using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.Account.Commands
{
    public class RegisterUserCommand : IRequest<Result<string>>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Role { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
        {
            private readonly IAccountService  _accountService;

            public RegisterUserCommandHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {

                return await _accountService.RegisterAsync(request.UserName, request.Password, request.Email,request.Role);
            }
        }
    }
}
