using ApplicationServices.OperationServices;
using CrossCutting;
using DomainModel.Operation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace dashboard_budget.HttpTriggers.OperationDashboard.CommonOperations
{
    public class GetOperationsOfLastDaysByPaidMethod
    {
        private readonly ILogger<GetOperationsOfLastDaysByPaidMethod> _logger;
        private readonly IOperationService operationService;

        public GetOperationsOfLastDaysByPaidMethod(ILogger<GetOperationsOfLastDaysByPaidMethod> logger, IOperationService service)
        {
            _logger = logger;
            this.operationService = service;
        }

        [Function("GetOperationsOfLastDaysByPaidMethod")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "operation/paidMethod")] HttpRequest req)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("GetOperationsOfLastDaysByPaidMethod Function HTTP Trigger | Start");

            int userId = !string.IsNullOrEmpty(req.Query["userId"]) ? Convert.ToInt32(req.Query["userId"]) : 0;

            if (userId <= 0 || string.IsNullOrEmpty(req.Query["paidMethod"]))
            {
                _logger.LogInformation("GetOperationsOfLastDaysByPaidMethod | User id is not valid");
                return new BadRequestResult();
            }

            DateTime dateToSearch;

            ServiceResponse<List<Operation>> response;

            if (!string.IsNullOrEmpty(req.Query["dateToSearch"]) && DateTime.TryParse(req.Query["dateToSearch"], out dateToSearch))
            {
                _logger.LogInformation($"GetOperationsOfLastDaysByPaidMethod | Datetime: {dateToSearch}");

                int field;

                PaidMethodOperation paidMethodOperation = default(PaidMethodOperation);

                if (int.TryParse(req.Query["paidMethod"], out field))
                {
                    paidMethodOperation = (PaidMethodOperation)field;
                }

                response = operationService.GetOperationsOfLastDaysByPaidMethod(userId, dateToSearch, paidMethodOperation);

                if (!response.IsSucess)
                {
                    return new BadRequestResult();
                }
            }
            else
            {
                _logger.LogInformation($"GetOperationsOfLastDaysByPaidMethod | DateTime is not valid");

                return new BadRequestResult();
            }

            _logger.LogInformation($"GetOperationsOfLastDaysByPaidMethod Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response);
        }
    }
}
