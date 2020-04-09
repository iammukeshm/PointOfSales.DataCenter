using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PointOfSales.DataCenter.Application.Features.Account.Commands;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost("register")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> RegisterAsync(RegisterUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}