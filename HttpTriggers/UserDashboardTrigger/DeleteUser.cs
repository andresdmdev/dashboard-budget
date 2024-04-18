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
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest req, [FromBody] UserDashboardDTO body)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("DeleteUser Function HTTP Trigger | Start");

            UserDashboard userDashboard = new UserDashboard()
            {
                Id = string.IsNullOrEmpty(body.Id) ? 0 : Convert.ToInt32(body.Id),
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

            ServiceResponse<bool> isDeleted = userService.DeleteUserDashboard(userDashboard);

            if (!isDeleted.IsSucess || !isDeleted.Entity)
            {
                return new BadRequestResult();
            }

            _logger.LogInformation($"DeleteUser Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(isDeleted);
        }
    }
}
