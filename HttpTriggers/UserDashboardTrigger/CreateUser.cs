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

namespace dashboard_budget.HttpTriggers.UserDashboardTrigger
{
    public class CreateUser
    {
        private readonly ILogger<CreateUser> _logger;
        private readonly IUserRepository _userRepository;

        public CreateUser(ILogger<CreateUser> logger, IUserRepository testRepository)
        {
            _logger = logger;
            _userRepository = testRepository;
        }

        [Function("CreateUser")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {

            UserDashboard userDashboard = new UserDashboard()
            {
                Name = "Andres",
                Email = "hola@gmail.com",
                Password = "password",
                Status = UserStatus.Active
            };

            int id = 4;
            // Funcionando
            //UserDashboard userCreated = _userRepository.Create(userDashboard);

            UserDashboard userCreated = _userRepository.Get(id);


            //List<UserDashboard> user = dBContext.UserDashboard.ToList();

            Thread.Sleep(2000);

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
