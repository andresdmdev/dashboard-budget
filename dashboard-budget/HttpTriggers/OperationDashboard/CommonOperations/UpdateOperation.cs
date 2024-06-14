using ApplicationServices.OperationServices;
using ApplicationServices.UserServices;
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
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace dashboard_budget.HttpTriggers.OperationDashboard.CommonOperations
{
    public class UpdateOperation
    {
        private readonly ILogger<UpdateOperation> _logger;
        private readonly IOperationService operationService;

        public UpdateOperation(ILogger<UpdateOperation> logger, IOperationService operation)
        {
            _logger = logger;
            operationService = operation;
        }

        [Function("UpdateOperation")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function,"put", Route = "operation/update")] HttpRequest req, [FromBody] OperationDashboardDTO body)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("UpdateOperation Function HTTP Trigger | Start");

            if(body == null || body.Id == 0)
            {
                _logger.LogInformation("UpdateOperation | Body is null");
                return new BadRequestResult();
            }

            Operation operationDashboard = new Operation()
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
                UpdatedBy = body.UpdatedBy,
                UpdatedDate = dateTime,
            };

            ServiceResponse<Operation> response = operationService.UpdateOperationDashboard(operationDashboard);

            if (!response.IsSucess || Operation.IsNullOrNew(response.Entity))
            {
                return new BadRequestResult();
            }

            _logger.LogInformation($"UpdateOperation Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response);
        }
    }
}
