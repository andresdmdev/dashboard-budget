using InfraestructureDB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using InfraestructureDB.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DomainModel.Operation;
using DomainModel.User;
using System.Diagnostics.Metrics;
using System.Net;
using dashboard_budget.DTOs;
using ApplicationServices.UserServices;

namespace dashboard_budget.HttpTriggers.UserDashboardTrigger
{
    public class CreateUser
    {
        private readonly ILogger<CreateUser> _logger;
        private readonly IUserService userService;

        public CreateUser(ILogger<CreateUser> logger, IUserService service)
        {
            _logger = logger;
            userService = service;
        }

        [Function("CreateUser")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, [FromBody] UserDashboardDTO body)
        {
            DateTime dateTime = DateTime.UtcNow;

            _logger.LogInformation("CreateUser Function HTTP Trigger | Start");

            UserDashboard userDashboard = new UserDashboard()
            {
                Name = body.Name,
                Email = body.Email,
                Password = body.Password,
                Status = UserStatus.Active,
                MobilePhone = body.MobilePhone,
                Address = body.Address,
                City = body.City,
                Country = body.Country,
                Age =  Convert.ToInt32(body.Age),
                CreatedBy = body.CreatedBy
            };

            UserDashboard user = userService.CreateUserDashboard(userDashboard);

            _logger.LogInformation($"CreateUser Function HTTP Trigger | End | ExecutionTime: {dateTime.ToString()}");
            return new OkObjectResult(user);
        }
        public record UserDashboardDTO
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? MobilePhone { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? Age { get; set; }
            public string? CreatedBy { get; set; }
            public string? UpdatedBy { get; set; }
            public string? DeletedBy { get; set; }
        }
    }
}
