using DashboardBudgetTest.FakeDataInfo;
using DomainModel.Operation;
using DomainModel.User;
using InfraestructureDB.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardBudgetTest.OperationTest
{
    public class OperationRepositoryTest
    {
        protected readonly DashboardDBContext _dbContext;
        private const string userName = "amarquez";
        private const int userId = 2;
        private const int daysToSearch = 3;
        public OperationRepositoryTest()
        {
            IQueryable<Operation> fakeOperations = FakeData.GetFakeOperationList().AsQueryable();

            Mock<DbSet<Operation>> operationMock = new Mock<DbSet<Operation>>();

            operationMock.As<IQueryable<Operation>>().Setup(o => o.Provider).Returns(fakeOperations.Provider);
            operationMock.As<IQueryable<Operation>>().Setup(o => o.Expression).Returns(fakeOperations.Expression);
            operationMock.As<IQueryable<Operation>>().Setup(o => o.ElementType).Returns(fakeOperations.ElementType);
            operationMock.As<IQueryable<Operation>>().Setup(o => o.GetEnumerator()).Returns(() => fakeOperations.GetEnumerator());

            var options = new DbContextOptionsBuilder<DashboardDBContext>()
                .UseInMemoryDatabase(databaseName: "Test Operation")
                .Options;

            _dbContext = new DashboardDBContext(options);
            _dbContext.OperationDashboard = operationMock.Object;
            _dbContext.SaveChanges();
        }

        [Fact]
        public void Create_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();

            try
            {
                _dbContext.OperationDashboard.Add(operation);
                _dbContext.SaveChanges();

                Assert.True(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Assert.True(false);
            }
        }

        [Fact]
        public void Update_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();
            Operation operationToUpdate = _dbContext.OperationDashboard.First(u => u.Id == operation.Id);

            if (operationToUpdate == null)
            {
                Console.WriteLine("Operation not found in the context");
                Assert.True(false);
            }

            operationToUpdate.Field = FieldOperation.Education;
            operationToUpdate.Amount = 34300;
            try
            {
                _dbContext.OperationDashboard.Update(operation);
                _dbContext.SaveChanges();

                Operation newOperation = _dbContext.OperationDashboard.First(u => u.Id == operation.Id);
                Assert.NotNull(newOperation);
                Assert.Equal(FieldOperation.Education, newOperation.Field);
                Assert.Equal(34300, newOperation.Amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Assert.True(false);
            }
        }

        [Fact]
        public void Delete_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();
            Operation operationToUpdate = _dbContext.OperationDashboard.First(u => u.Id == operation.Id);

            if (operationToUpdate == null)
            {
                Console.WriteLine("Operation not found in the context");
                Assert.True(false);
            }
            DateTime dateTime = DateTime.Now;
            operationToUpdate.DeletedBy = userName;
            operationToUpdate.DeletedDate = dateTime;
            try
            {
                _dbContext.OperationDashboard.Update(operation);
                _dbContext.SaveChanges();

                Operation newOperation = _dbContext.OperationDashboard.First(u => u.Id == operation.Id);
                Assert.NotNull(newOperation);
                Assert.Equal(userName, newOperation.DeletedBy);
                Assert.Equal(dateTime, newOperation.DeletedDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Assert.True(false);
            }
        }

        [Fact]
        public void Get_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().Last();
            try
            {
                Operation operationDashboard = _dbContext.OperationDashboard.Where(o => o.Id == operation.Id).First();

                Assert.NotNull(operationDashboard);
                Assert.Equal(operation.Id, operationDashboard.Id);
                Assert.Equal(operation.Status, operationDashboard.Status);
                Assert.Equal(operation.Field, operationDashboard.Field);
                Assert.Equal(operation.PaidMethod, operationDashboard.PaidMethod);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update - {ex.Message}");
                Assert.True(false, $"Exception thrown: {ex.Message}");
            }
        }

        [Fact]
        public void GetOperationsOfLastDays_Test()
        {
            DateTime dateTime = DateTime.Now.AddDays(-daysToSearch);
            try
            {
                List<Operation> operations = _dbContext.OperationDashboard.Where(op => op.UserId == userId &&
                    op.Date.Date >= dateTime.Date &&
                    op.DeletedDate == null &&
                    op.DeletedBy == null
                    ).ToList();

                Assert.NotNull(operations);
                Assert.Single(operations);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOperationsOfLastDays - {ex.Message}");
                Assert.True(false);
            }
        }

        [Fact]
        public void GetOperationsOfLastDaysByStatus_Test()
        {
            DateTime dateTime = DateTime.Now.AddDays(-daysToSearch);
            StatusOperation statusOperation = StatusOperation.Paid;
            try
            {
                List<Operation> operations = _dbContext.OperationDashboard.Where(op => op.UserId == userId &&
                    op.Date.Date >= dateTime.Date &&
                    op.Status == statusOperation &&
                    op.DeletedDate == null &&
                    op.DeletedBy == null
                    ).ToList();

                Assert.NotNull(operations);
                Assert.Single(operations);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOperationsOfLastDaysByStatus - {ex.Message}");
                Assert.True(false);
            }
        }

        [Fact]
        public void GetOperationsOfLastDaysByField_Test()
        {
            DateTime dateTime = DateTime.Now.AddDays(-daysToSearch);
            FieldOperation fieldOperation = FieldOperation.Shooping;
            try
            {
                List<Operation> operations = _dbContext.OperationDashboard.Where(op => op.UserId == userId &&
                    op.Date.Date >= dateTime.Date &&
                    op.Field == fieldOperation &&
                    op.DeletedDate == null &&
                    op.DeletedBy == null
                    ).ToList();

                Assert.NotNull(operations);
                Assert.Single(operations);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOperationsOfLastDaysByField - {ex.Message}");
                Assert.True(false);
            }
        }

        [Fact]
        public void GetOperationsOfLastDaysByPaidMethod_Test()
        {
            DateTime dateTime = DateTime.Now.AddDays(-daysToSearch);
            PaidMethodOperation paidMethodOperation = PaidMethodOperation.CreditCard;
            try
            {
                List<Operation> operations = _dbContext.OperationDashboard.Where(op => op.UserId == userId &&
                    op.Date.Date >= dateTime.Date &&
                    op.PaidMethod == paidMethodOperation &&
                    op.DeletedDate == null &&
                    op.DeletedBy == null
                    ).ToList();

                Assert.NotNull(operations);
                Assert.Single(operations);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOperationsOfLastDaysByPaidMethod - {ex.Message}");
                Assert.True(false);
            }
        }

    }
}
