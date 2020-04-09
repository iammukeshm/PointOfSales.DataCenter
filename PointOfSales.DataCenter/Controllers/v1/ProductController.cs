using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSales.DataCenter.Application.Features.ProductFeatures.Commands;

namespace PointOfSales.DataCenter.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductController : CoreController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}