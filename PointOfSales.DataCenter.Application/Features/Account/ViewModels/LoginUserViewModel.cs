using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Application.Features.Account.ViewModels
{
    public class LoginUserViewModel
    {
        public bool? HasVerifiedEmail { get; set; }
        public bool? TFAEnabled { get; set; }
        public string Token { get; set; }
    }
}
