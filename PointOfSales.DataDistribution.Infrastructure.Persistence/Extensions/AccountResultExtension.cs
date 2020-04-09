using Microsoft.AspNetCore.Identity;
using PointOfSales.DataDistribution.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSales.DataDistribution.Infrastructure.Persistence.Extensions
{
    public static class AccountResultExtension
    {
        public static Result<string> ToApplicationResult(this IdentityResult result, string message, string data)
        {
            return result.Succeeded
                ? Result<string>.Success(data)
                : Result<string>.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
