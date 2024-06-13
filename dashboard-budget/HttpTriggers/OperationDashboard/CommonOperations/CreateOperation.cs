using CrossCutting;
using dashboard_budget.DTOs.Operation;
using DomainModel.Operation;
using DomainModel.User;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using ApplicationServices.OperationServices;

namespace dashboard_budget.HttpTriggers.OperationDashboard.CommonOperations
{
    public class CreateOperation
    {
        private readonly ILogger<CreateOperation> _logger;
        private readonly IOperationService operationService;

        public CreateOperation(ILogger<CreateOperation> logger, IOperationService operation)
        {
            _logger = logger;
            operationService = operation;
        }

        [Function("CreateOperation")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "operation/create")] HttpRequest req, [FromBody] OperationDashboardDTO body)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("CreateOperation Function HTTP Trigger | Start");

            Operation userDashboard = new Operation()
            {
                Id = body.Id,
                UserId = body.UserId,
                Status = body.Status,
                Date = body.Date,
                Amount = body.Amount,
                Field = body.Field,
                Comments = body.Comments,
                PaidDate = body.PaidDate,
                QuoteAmount = body.QuoteAmount,
                QuoteFee = body.QuoteFee,
                QuoteNumber = body.QuoteNumber,
                PaidDestination = body.PaidDestination,
                PaidMethod = body.PaidMethod,
                CreatedBy = body.CreatedBy
            };

            ServiceResponse<Operation> response = operationService.CreateOperationDashboard(userDashboard);

            if (!response.IsSucess || UserDashboard.IsNullOrNew(response.Entity))
            {
                return new BadRequestObjectResult(response);
            }

            _logger.LogInformation($"CreateOperation Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response);
        }
    }
}
