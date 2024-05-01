using ApplicationServices.UserServices;
using CrossCutting;
using dashboard_budget.DTOs;
using DomainModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace dashboard_budget.HttpTriggers.UserDashboardTrigger
{
    public class DeleteUser
    {
        private readonly ILogger<DeleteUser> _logger;
        private readonly IUserService userService;

        public DeleteUser(ILogger<DeleteUser> logger, IUserService service)
        {
            _logger = logger;
            userService = service;
        }

        [Function("DeleteUser")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("DeleteUser Function HTTP Trigger | Start");

            int userId = !string.IsNullOrEmpty(req.Query["id"]) ? Convert.ToInt32(req.Query["id"]) : 0;

            string? deleteBy = !string.IsNullOrEmpty(req.Query["deleteBy"]) ? req.Query["deleteBy"] : "";

            if (userId == 0)
            {
                _logger.LogError("DeleteUser Function HTTP Trigger | Id is null");
            }

            ServiceResponse<bool> isDeleted = userService.DeleteUserDashboard(userId, deleteBy ?? "");

            if (!isDeleted.IsSucess || !isDeleted.Entity)
            {
                return new BadRequestResult();
            }

            _logger.LogInformation($"DeleteUser Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(isDeleted.Entity);
        }
    }
}
