using ApplicationServices.OperationServices;
using ApplicationServices.UserServices;
using CrossCutting;
using DashboardBudgetTest.FakeDataInfo;
using DomainModel.Operation;
using InfraestructureDB.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardBudgetTest.OperationTest
{
    public class OperationServiceTest
    {
        private readonly ILogger<OperationService> log;
        private readonly OperationService operationService;
        private readonly Mock<IOperationRepository> _operationRepository;
        private const string userName = "amarquez";
        private const int userId = 2;
        private const int daysToSearch = 3;

        public OperationServiceTest()
        {
            log = Mock.Of<ILogger<OperationService>>();
            _operationRepository = new Mock<IOperationRepository>();
            operationService = new OperationService(log, _operationRepository.Object);
        }

        [Fact]
        public void CreateOperationDashboard_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();

            _operationRepository.Setup(o => o.Create(operation)).Returns(operation);

            ServiceResponse<Operation> serviceResponse = operationService.CreateOperationDashboard(operation);

            Assert.True(serviceResponse.IsSucess);
            Assert.Equal(serviceResponse.Entity, operation);
        }

        [Fact]
        public void UpdateOperationDashboard_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();

            operation.Amount = 6000;
            operation.PaidMethod = PaidMethodOperation.ExternalLoan;
            operation.Status = StatusOperation.Paid;

            _operationRepository.Setup(o => o.Update(operation, userName)).Returns(operation);

            ServiceResponse<Operation> serviceResponse = operationService.UpdateOperationDashboard(operation);

            Assert.True(serviceResponse.IsSucess);
            Assert.Equal(serviceResponse.Entity, operation);
        }

        [Fact]
        public void GetOperationDashboard_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();

            _operationRepository.Setup(o => o.Get(operation.Id)).Returns(operation);

            ServiceResponse<Operation> serviceResponse = operationService.GetOperationDashboard(operation.Id);

            Assert.True(serviceResponse.IsSucess);
            Assert.Equal(serviceResponse.Entity, operation);
        }

        [Fact]
        public void GetOperationDashboard_WhenIsDeleted_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();
            operation.DeletedBy = userName;
            operation.DeletedDate = DateTime.Now.AddDays(-1);

            _operationRepository.Setup(o => o.Get(operation.Id)).Returns(operation);

            ServiceResponse<Operation> serviceResponse = operationService.GetOperationDashboard(operation.Id);

            Assert.True(serviceResponse.IsSucess);
            Assert.Null(serviceResponse.Entity);
        }

        [Fact]
        public void DeleteOperationDashboard_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();
            operation.DeletedBy = userName;
            operation.DeletedDate = DateTime.Now.AddDays(-1);

            _operationRepository.Setup(o => o.Get(operation.Id)).Returns(operation);
            _operationRepository.Setup(o => o.Delete(operation, userName)).Returns(true);

            ServiceResponse<bool> serviceResponse = operationService.DeleteOperationDashboard(operation.Id, userName);

            Assert.True(serviceResponse.IsSucess);
            Assert.True(serviceResponse.Entity);
        }

        [Fact]
        public void DeleteOperationDashboard_WhenOperationIsNotFound_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();
            operation.DeletedBy = userName;
            operation.DeletedDate = DateTime.Now.AddDays(-1);

            _operationRepository.Setup(o => o.Get(operation.Id)).Returns(default(Operation));
            _operationRepository.Setup(o => o.Delete(operation, userName)).Returns(false);

            ServiceResponse<bool> serviceResponse = operationService.DeleteOperationDashboard(operation.Id, userName);

            Assert.False(serviceResponse.IsSucess);
            Assert.False(serviceResponse.Entity);
        }

        [Fact]
        public void GetOperationsOfLastDaysByNumberOfDays_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();
            List<Operation> opertationList = new List<Operation> { operation };
            _operationRepository.Setup(o => o.GetOperationsOfLastDays(operation.UserId, DateTime.Now.AddDays(-daysToSearch).Date)).Returns(opertationList);

            ServiceResponse<List<Operation>> serviceResponse = operationService.GetOperationsOfLastDays(operation.UserId, daysToSearch);

            Assert.True(serviceResponse.IsSucess);
            Assert.Single(serviceResponse.Entity);
        }

        [Fact]
        public void GetOperationsOfLastDaysByDateTime_Test()
        {
            Operation operation = FakeData.GetFakeOperationList().First();
            List<Operation> opertationList = new List<Operation> { operation };
            DateTime dateTime = DateTime.Now.AddDays(-daysToSearch).Date;
            _operationRepository.Setup(o => o.GetOperationsOfLastDays(operation.UserId, dateTime)).Returns(opertationList);

            ServiceResponse<List<Operation>> serviceResponse = operationService.GetOperationsOfLastDays(operation.UserId, dateTime);

            Assert.True(serviceResponse.IsSucess);
            Assert.Single(serviceResponse.Entity);
        }

        [Fact]
        public void GetOperationsOfLastDaysByStatus_Test()
        {
            StatusOperation statusOperation = StatusOperation.Paid;
            Operation operation = FakeData.GetFakeOperationList().Where(o => o.Status == statusOperation).First();
            List<Operation> opertationList = new List<Operation> { operation };
            DateTime dateTime = DateTime.Now.AddDays(-daysToSearch).Date;
            _operationRepository.Setup(o => o.GetOperationsOfLastDaysByStatus(operation.UserId, dateTime, statusOperation)).Returns(opertationList);

            ServiceResponse<List<Operation>> serviceResponse = operationService.GetOperationsOfLastDaysByStatus(operation.UserId, dateTime, statusOperation);

            Assert.True(serviceResponse.IsSucess);
            Assert.Single(serviceResponse.Entity);
        }

        [Fact]
        public void GetOperationsOfLastDaysByField_Test()
        {
            FieldOperation fieldOperation = FieldOperation.Investment;
            Operation operation = FakeData.GetFakeOperationList().Where(o => o.Field == fieldOperation).First();
            List<Operation> opertationList = new List<Operation> { operation };
            DateTime dateTime = DateTime.Now.AddDays(-daysToSearch).Date;
            _operationRepository.Setup(o => o.GetOperationsOfLastDaysByField(operation.UserId, dateTime, fieldOperation)).Returns(opertationList);

            ServiceResponse<List<Operation>> serviceResponse = operationService.GetOperationsOfLastDaysByField(operation.UserId, dateTime, fieldOperation);

            Assert.True(serviceResponse.IsSucess);
            Assert.Single(serviceResponse.Entity);
        }

        [Fact]
        public void GetOperationsOfLastDaysByPaidMethod_Test()
        {
            PaidMethodOperation paidMethodOperation = PaidMethodOperation.DebitCard;
            Operation operation = FakeData.GetFakeOperationList().Where(o => o.PaidMethod == paidMethodOperation).First();
            List<Operation> opertationList = new List<Operation> { operation };
            DateTime dateTime = DateTime.Now.AddDays(-daysToSearch).Date;
            _operationRepository.Setup(o => o.GetOperationsOfLastDaysByPaidMethod(operation.UserId, dateTime, paidMethodOperation)).Returns(opertationList);

            ServiceResponse<List<Operation>> serviceResponse = operationService.GetOperationsOfLastDaysByPaidMethod(operation.UserId, dateTime, paidMethodOperation);

            Assert.True(serviceResponse.IsSucess);
            Assert.Single(serviceResponse.Entity);
        }
    }
}
