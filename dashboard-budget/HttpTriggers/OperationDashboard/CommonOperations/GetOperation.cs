using ApplicationServices.OperationServices;
using CrossCutting;
using DomainModel.Operation;
using DomainModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace dashboard_budget.HttpTriggers.OperationDashboard.CommonOperations
{
    public class GetOperation
    {
        private readonly ILogger<GetOperation> _logger;
        private readonly IOperationService operationService;

        public GetOperation(ILogger<GetOperation> logger, IOperationService _operationService)
        {
            _logger = logger;
            operationService = _operationService;
        }

        [Function("GetOperation")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "operation")] HttpRequest req)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("GetOperation Function HTTP Trigger | Start");

            int userId = !string.IsNullOrEmpty(req.Query["id"]) ? Convert.ToInt32(req.Query["id"]) : 0;

            if (userId == 0)
            {
                _logger.LogError("GetOperation Function HTTP Trigger | Id is null");
            }

            ServiceResponse<Operation> response = operationService.GetOperationDashboard(userId);

            if (!response.IsSucess || UserDashboard.IsNullOrNew(response.Entity))
            {
                return new BadRequestObjectResult(response);
            }

            _logger.LogInformation($"GetOperation Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response);
        }
    }
}
