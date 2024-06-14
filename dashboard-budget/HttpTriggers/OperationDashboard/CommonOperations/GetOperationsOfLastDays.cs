using ApplicationServices.OperationServices;
using CrossCutting;
using DomainModel.Operation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace dashboard_budget.HttpTriggers.OperationDashboard.CommonOperations
{
    public class GetOperationsOfLastDays
    {
        private readonly ILogger<GetOperationsOfLastDays> _logger;
        private readonly IOperationService operationService;

        public GetOperationsOfLastDays(ILogger<GetOperationsOfLastDays> logger, IOperationService service)
        {
            _logger = logger;
            operationService = service;
        }

        [Function("GetOperationsOfLastDays")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "operation/lastdays")] HttpRequest req)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("GetOperationsOfLastDays Function HTTP Trigger | Start");

            int userId = !string.IsNullOrEmpty(req.Query["userId"]) ? Convert.ToInt32(req.Query["userId"]) : 0;
            
            if (userId <= 0)
            {
                _logger.LogInformation("GetOperationsOfLastDays | User id is not valid");
                return new BadRequestResult();
            }

            DateTime dateToSearch;

            ServiceResponse<List<Operation>> response;

            if (!string.IsNullOrEmpty(req.Query["dateToSearch"]) && DateTime.TryParse(req.Query["dateToSearch"], out dateToSearch))
            {
                _logger.LogInformation($"GetOperationsOfLastDays | Datetime: {dateToSearch}");

                response = operationService.GetOperationsOfLastDays(userId, dateToSearch);
            }
            else
            {
                int daysToSearch = !string.IsNullOrEmpty(req.Query["daysToSearch"]) ? Convert.ToInt32(req.Query["daysToSearch"]) : 1;

                response = operationService.GetOperationsOfLastDays(userId, daysToSearch);
            }

            if (!response.IsSucess)
            {
                return new BadRequestResult();
            }

            _logger.LogInformation($"GetOperationsOfLastDays Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response);
        }
    }
}
