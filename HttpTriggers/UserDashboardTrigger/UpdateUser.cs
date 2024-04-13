using ApplicationServices.UserServices;
using dashboard_budget.DTOs;
using DomainModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dashboard_budget.HttpTriggers.UserDashboardTrigger
{
    public class UpdateUser
    {
        private readonly ILogger<UpdateUser> _logger;
        private readonly IUserService userService;

        public UpdateUser(ILogger<UpdateUser> logger, IUserService service)
        {
            _logger = logger;
            userService = service;
        }

        [Function("UpdateUser")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest req, [FromBody] UserDashboardDTO body)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("UpdateUser Function HTTP Trigger | Start");

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

            UserDashboard user = userService.UpdateUserDashboard(userDashboard);

            if (UserDashboard.IsNullOrNew(user))
            {
                return new BadRequestResult();
            }

            _logger.LogInformation($"UpdateUser Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(user);
        }
    }
}
