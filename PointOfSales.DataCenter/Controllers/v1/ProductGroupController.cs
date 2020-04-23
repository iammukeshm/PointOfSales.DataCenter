using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSales.DataCenter.Application.Features.Product.Queries;

namespace PointOfSales.DataCenter.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductGroupController : CoreController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllProductGroupsQuery()));
        }
    }
}