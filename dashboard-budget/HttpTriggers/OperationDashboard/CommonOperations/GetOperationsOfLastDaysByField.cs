using ApplicationServices.OperationServices;
using CrossCutting;
using DomainModel.Operation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace dashboard_budget.HttpTriggers.OperationDashboard.CommonOperations
{
    public class GetOperationsOfLastDaysByField
    {
        private readonly IOperationService operationService;
        private readonly ILogger<GetOperationsOfLastDaysByField> _logger;

        public GetOperationsOfLastDaysByField(ILogger<GetOperationsOfLastDaysByField> logger, IOperationService service)
        {
            _logger = logger;
            this.operationService = service;
        }

        [Function("GetOperationsOfLastDaysByField")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "operation/field")] HttpRequest req)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("GetOperationsOfLastDaysByField Function HTTP Trigger | Start");

            int userId = !string.IsNullOrEmpty(req.Query["userId"]) ? Convert.ToInt32(req.Query["userId"]) : 0;

            if (userId <= 0 || string.IsNullOrEmpty(req.Query["field"]))
            {
                _logger.LogInformation("GetOperationsOfLastDaysByField | User id is not valid");
                return new BadRequestResult();
            }

            DateTime dateToSearch;

            ServiceResponse<List<Operation>> response;

            if (!string.IsNullOrEmpty(req.Query["dateToSearch"]) && DateTime.TryParse(req.Query["dateToSearch"], out dateToSearch))
            {
                _logger.LogInformation($"GetOperationsOfLastDaysByField | Datetime: {dateToSearch}");

                int field;

                FieldOperation fieldOperation = default(FieldOperation);

                if (int.TryParse(req.Query["field"], out field))
                {
                    fieldOperation = (FieldOperation)field;
                }

                response = operationService.GetOperationsOfLastDaysByField(userId, dateToSearch, fieldOperation);

                if (!response.IsSucess)
                {
                    return new BadRequestResult();
                }
            }
            else
            {
                _logger.LogInformation($"GetOperationsOfLastDaysByField | DateTime is not valid");

                return new BadRequestResult();
            }

            _logger.LogInformation($"GetOperationsOfLastDaysByField Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response);
        }
    }
}
