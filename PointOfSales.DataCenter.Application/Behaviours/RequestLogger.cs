using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using PointOfSales.DataCenter.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAccountService _accountService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService, IAccountService accountService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _accountService = accountService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _accountService.GetUserNameAsync(userId);
            }
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogInformation($"DataCenter Request by Anonymous - {requestName} with Parameters {jsonString}");
            }
            else
            {
                _logger.LogInformation($"DataCenter Request by {userName} {userId} - {requestName} with Parameters {jsonString}");
            }
            
            
        }
    }
}
