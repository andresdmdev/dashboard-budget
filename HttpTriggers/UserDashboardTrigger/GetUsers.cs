using ApplicationServices.UserServices;
using CrossCutting;
using dashboard_budget.DTOs;
using DomainModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
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

        [Function("GetUsers")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, [FromBody] UserDashboardDTO body)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("CreateUser Function HTTP Trigger | Start");

            UserDashboard userDashboard = new UserDashboard()
            {
                Name = body.Name,
                Email = string.IsNullOrEmpty(body.Email) ? string.Empty : body.Email,
                Password = string.IsNullOrEmpty(body.Password) ? string.Empty : body.Password,
                Status = UserStatus.Active,
                MobilePhone = body.MobilePhone,
                Address = body.Address,
                City = body.City,
                Country = body.Country,
                Age = Convert.ToInt32(body.Age),
                CreatedBy = body.CreatedBy
            };

            ServiceResponse<UserDashboard> response = userService.GetUserDashboard(userDashboard.Id);

            if (!response.IsSucess || UserDashboard.IsNullOrNew(response.Entity))
            {
                return new BadRequestObjectResult(response);
            }

            _logger.LogInformation($"CreateUser Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(response);
        }
    }
}
