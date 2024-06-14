using ApplicationServices.UserServices;
using Castle.Core.Logging;
using CrossCutting;
using DashboardBudgetTest.FakeDataInfo;
using DomainModel.User;
using InfraestructureDB.Repository;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DashboardBudgetTest.User
{
    public class UserServiceTest
    {
        private readonly ILogger<UserService> log;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly UserService userService;
        private const string userName = "amarquez";

        public UserServiceTest()
        {
            log = Mock.Of<ILogger<UserService>>();
            _userRepository = new Mock<IUserRepository>();
            userService = new UserService(log, _userRepository.Object);
        }

        [Fact]
        public void CreateUserDashboardTest()
        {
            UserDashboard userDashboard = FakeData.GetFakeUserDashboardList().First();

            _userRepository.Setup(repo => repo.Create(userDashboard)).Returns(userDashboard);

            ServiceResponse<UserDashboard> serviceResponse = userService.CreateUserDashboard(userDashboard);

            Assert.True(serviceResponse.IsSucess);
            Assert.Equal(userDashboard, serviceResponse.Entity);
        }
        [Fact]
        public void DeleteUserDashboardTest()
        {
            UserDashboard userDashboard = FakeData.GetFakeUserDashboardList().First();

            _userRepository.Setup(repo => repo.Get(userDashboard.Id)).Returns(userDashboard);
            _userRepository.Setup(repo => repo.Delete(userDashboard, userName)).Returns(true);

            ServiceResponse<bool> serviceResponse = userService.DeleteUserDashboard(userDashboard.Id, userName);

            Assert.True(serviceResponse.IsSucess);
            Assert.True(serviceResponse.Entity);
        }
        [Fact]
        public void UpdateUserDashboardTest()
        {
            UserDashboard userDashboard = FakeData.GetFakeUserDashboardList().First();

            _userRepository.Setup(repo => repo.Update(userDashboard, userName)).Returns(userDashboard);

            ServiceResponse<UserDashboard> serviceResponse = userService.UpdateUserDashboard(userDashboard);

            Assert.True(serviceResponse.IsSucess);
            Assert.Equal(userDashboard, serviceResponse.Entity);
        }
        [Fact]
        public void GetUserDashboardTest()
        {
            UserDashboard userDashboard = FakeData.GetFakeUserDashboardList().First();

            _userRepository.Setup(repo => repo.Get(userDashboard.Id)).Returns(userDashboard);

            ServiceResponse<UserDashboard> serviceResponse = userService.GetUserDashboard(userDashboard.Id);

            Assert.True(serviceResponse.IsSucess);
            Assert.Equal(userDashboard, serviceResponse.Entity);
        }
    }
}
