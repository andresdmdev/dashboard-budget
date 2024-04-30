using ApplicationServices.UserServices;
using CrossCutting;
using DomainModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace dashboard_budget.HttpTriggers.UserDashboardTrigger
{
    public class GetUsers
    {
        private readonly ILogger<GetUsers> _logger;
        private readonly IUserService userService;

        public GetUsers(ILogger<GetUsers> logger, IUserService service)
        {
            _logger = logger;
            userService = service;
        }

        [Function("GetUser")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("CreateUser Function HTTP Trigger | Start");

            int userId = !string.IsNullOrEmpty(req.Query["id"]) ? Convert.ToInt32(req.Query["id"]) : 0;

            if(userId == 0)
            {
                _logger.LogError("CreateUser Function HTTP Trigger | Id is null");
            }

            ServiceResponse<UserDashboard> response = userService.GetUserDashboard(userId);

            if (!response.IsSucess || UserDashboard.IsNullOrNew(response.Entity))
            {
                return new BadRequestObjectResult(response);
            }

            _logger.LogInformation($"CreateUser Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response.Entity);
        }
    }
}
