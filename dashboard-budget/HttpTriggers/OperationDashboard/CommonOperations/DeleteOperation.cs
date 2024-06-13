using ApplicationServices.OperationServices;
using CrossCutting;
using DomainModel.Operation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace dashboard_budget.HttpTriggers.OperationDashboard.CommonOperations
{
    public class DeleteOperation
    {
        private readonly ILogger<DeleteOperation> _logger;
        private readonly IOperationService operationService;

        public DeleteOperation(ILogger<DeleteOperation> logger, IOperationService operation)
        {
            _logger = logger;
            operationService = operation;
        }

        [Function("DeleteOperation")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "operation/delete")] HttpRequest req)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("GetOperation Function HTTP Trigger | Start");

            int operationId = !string.IsNullOrEmpty(req.Query["id"]) ? Convert.ToInt32(req.Query["id"]) : 0;

            string? deleteBy = !string.IsNullOrEmpty(req.Query["deleteBy"]) ? req.Query["deleteBy"] : "";

            if (operationId == 0)
            {
                _logger.LogError("GetOperation Function HTTP Trigger | Id is null");
                return new BadRequestResult();
            }

            ServiceResponse<bool> isDeleted = operationService.DeleteOperationDashboard(operationId, deleteBy ?? "");

            if (!isDeleted.IsSucess || !isDeleted.Entity)
            {
                return new BadRequestResult();
            }

            _logger.LogInformation($"GetOperation Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(isDeleted);
        }
    }
}
