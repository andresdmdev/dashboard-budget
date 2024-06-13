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
        public void UpdateOperationDashboard_Test() {}
        [Fact]
        public void GetOperationDashboard_Test() {}
        [Fact]
        public void DeleteOperationDashboard_Test() {}
        [Fact]
        public void GetOperationsOfLastDaysByNumberOfDays_Test() {}
        [Fact]
        public void GetOperationsOfLastDaysByDateTime_Test() {}
        [Fact]
        public void GetOperationsOfLastDaysByStatus_Test() {}
        [Fact]
        public void GetOperationsOfLastDaysByField_Test() {}
        [Fact]
        public void GetOperationsOfLastDaysByPaidMethod_Test() {}

    }
}
