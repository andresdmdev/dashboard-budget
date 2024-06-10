using ApplicationServices.OperationServices;
using CrossCutting;
using DomainModel.Operation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace dashboard_budget.HttpTriggers.OperationDashboard.CommonOperations
{
    public class GetOperationsOfLastDaysByStatus
    {
        private readonly ILogger<GetOperationsOfLastDaysByStatus> _logger;
        private readonly IOperationService operationService;

        public GetOperationsOfLastDaysByStatus(ILogger<GetOperationsOfLastDaysByStatus> logger, IOperationService service)
        {
            _logger = logger;
            this.operationService = service;
        }

        [Function("GetOperationsOfLastDaysByStatus")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "operation/status")] HttpRequest req)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("GetOperationsOfLastDaysByStatus Function HTTP Trigger | Start");

            int userId = !string.IsNullOrEmpty(req.Query["userId"]) ? Convert.ToInt32(req.Query["userId"]) : 0;

            if (userId <= 0 || string.IsNullOrEmpty(req.Query["status"]))
            {
                _logger.LogInformation("GetOperationsOfLastDaysByStatus | User id is not valid");
                return new BadRequestResult();
            }

            DateTime dateToSearch;

            ServiceResponse<List<Operation>> response;

            if (!string.IsNullOrEmpty(req.Query["dateToSearch"]) && DateTime.TryParse(req.Query["dateToSearch"], out dateToSearch))
            {
                _logger.LogInformation($"GetOperationsOfLastDaysByStatus | Datetime: {dateToSearch}");

                int status;

                StatusOperation statusOperation = default(StatusOperation);

                if (int.TryParse(req.Query["status"], out status))
                {
                    statusOperation = (StatusOperation)status;
                }

                response = operationService.GetOperationsOfLastDaysByStatus(userId, dateToSearch, statusOperation);

                if (!response.IsSucess)
                {
                    return new BadRequestResult();
                }
            }
            else
            {
                _logger.LogInformation($"GetOperationsOfLastDaysByStatus | DateTime is not valid");

                return new BadRequestResult();
            }

            _logger.LogInformation($"GetOperationsOfLastDaysByStatus Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response);
        }
    }
}
