using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSales.DataCenter.Application.Features.Invoice.Commands;

namespace PointOfSales.DataCenter.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class InvoiceController : CoreController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateInvoiceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}