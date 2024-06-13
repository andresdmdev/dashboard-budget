using ApplicationServices.UserServices;
using DashboardBudgetTest.FakeDataInfo;
using DomainModel;
using DomainModel.User;
using InfraestructureDB.Context;
using InfraestructureDB.Repository;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DashboardBudgetTest.User
{
    public class UserRepositoryTest
    {
        protected DashboardDBContext dBContext { get; set; }
        protected List<UserDashboard> users { get; set; }

        public UserRepositoryTest()
        {
            users = FakeData.GetFakeUserDashboardList();
            IQueryable<UserDashboard> userDashboard = users.AsQueryable();

            var mockSet = new Mock<DbSet<UserDashboard>>();

            mockSet.As<IQueryable<UserDashboard>>().Setup(m => m.Provider).Returns(userDashboard.Provider);
            mockSet.As<IQueryable<UserDashboard>>().Setup(m => m.Expression).Returns(userDashboard.Expression);
            mockSet.As<IQueryable<UserDashboard>>().Setup(m => m.ElementType).Returns(userDashboard.ElementType);
            mockSet.As<IQueryable<UserDashboard>>().Setup(m => m.GetEnumerator()).Returns(() => userDashboard.GetEnumerator());

            var options = new DbContextOptionsBuilder<DashboardDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dBContext = new DashboardDBContext(options);
            dBContext.UserDashboard = mockSet.Object;
            dBContext.SaveChanges();
        }

        [Fact]
        public void CreateTest() 
        {
            UserDashboard userDashboard = FakeData.GetFakeUserDashboardList().First();

            try
            {
                dBContext.UserDashboard.Add(userDashboard);

                dBContext.SaveChanges();
                
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create - {ex.Message}");
                Assert.True(false);
            }
        }

        [Fact]
        public void UpdateTest()
        {
            UserDashboard originalUser = FakeData.GetFakeUserDashboardList().Last();

            UserDashboard userToUpdate = dBContext.UserDashboard.First(u => u.Id == originalUser.Id);

            if (userToUpdate == null)
            {
                Console.WriteLine("User not found in the context");
                Assert.True(false);
            }

            userToUpdate.Name = "Updated Name";
            userToUpdate.Email = "updated.email@example.com";

            try
            {
                dBContext.UserDashboard.Update(userToUpdate);
                dBContext.SaveChanges();

                UserDashboard updatedUser = dBContext.UserDashboard.First(u => u.Id == originalUser.Id);
                Assert.NotNull(updatedUser);
                Assert.Equal("Updated Name", updatedUser.Name);
                Assert.Equal("updated.email@example.com", updatedUser.Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update - {ex.Message}");
                Assert.True(false, $"Exception thrown: {ex.Message}");
            }
        }

        [Fact]
        public void DeleteTest()
        {
            DateTime deletedDate = DateTime.Now;
            UserDashboard originalUser = FakeData.GetFakeUserDashboardList().Last();

            UserDashboard userToUpdate = dBContext.UserDashboard.First(u => u.Id == originalUser.Id);

            if (userToUpdate == null)
            {
                Console.WriteLine("User not found in the context");
                Assert.True(false);
            }

            userToUpdate.DeletedBy = "amarquez";
            userToUpdate.DeletedDate = deletedDate;

            try
            {
                dBContext.UserDashboard.Update(userToUpdate);
                dBContext.SaveChanges();

                UserDashboard updatedUser = dBContext.UserDashboard.Where(u => u.Id == originalUser.Id).First();
                Assert.NotNull(updatedUser);
                Assert.Equal("amarquez", updatedUser.DeletedBy);
                Assert.Equal(deletedDate, updatedUser.DeletedDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update - {ex.Message}");
                Assert.True(false, $"Exception thrown: {ex.Message}");
            }
        }

        [Fact]
        public void GetTest()
        {
            UserDashboard originalUser = FakeData.GetFakeUserDashboardList().Last();
            try
            {
                UserDashboard userDashboard = dBContext.UserDashboard.Where(u => u.Id == originalUser.Id).First();

                Assert.NotNull(userDashboard);
                Assert.Equal(originalUser.Id, userDashboard.Id);
                Assert.Equal(originalUser.Email, userDashboard.Email);
                Assert.Equal(originalUser.Name, userDashboard.Name);
                Assert.Equal(originalUser.Password, userDashboard.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update - {ex.Message}");
                Assert.True(false, $"Exception thrown: {ex.Message}");
            }
        }

    }
}
