using DomainModel.Operation;
using DomainModel.User;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace DashboardBudgetTest.FakeDataInfo
{
    public static class FakeData
    {
        public static List<UserDashboard> GetFakeUserDashboardList()
        {
            return new List<UserDashboard>()
            {
                new UserDashboard()
                {
                    Id = 1,
                    Name = "Andres",
                    Email = "andresdmf55@gmail.com",
                    Password = "password",
                    Age = 25,
                    CreatedBy = "amarquez",
                    CreatedDate = DateTime.Now,
                },
                new UserDashboard()
                {
                    Id = 2,
                    Name = "Cesar",
                    Email = "cesarone@gmail.com",
                    Password = "escanor",
                    Age = 22,
                    CreatedBy = "cmarquez",
                    CreatedDate = DateTime.Now,
                }
            };
        }
        public static List<Operation> GetFakeOperationList()
        {
            return new List<Operation>()
            {
                new Operation()
                {
                    Id= 1,
                    UserId = 1,
                    Status = StatusOperation.NotPaid,
                    Date = DateTime.Now,
                    Amount = 3000,
                    Field = FieldOperation.Investment,
                    Comments = null,
                    PaidDate = DateTime.Now.AddDays(2),
                    QuoteAmount = null,
                    QuoteFee = null,
                    QuoteNumber = null,
                    PaidDestination = null,
                    PaidMethod = PaidMethodOperation.DebitCard
                },
                new Operation()
                {
                    Id= 2,
                    UserId = 2,
                    Status = StatusOperation.Paid,
                    Date = DateTime.Now,
                    Amount = 5000,
                    Field = FieldOperation.Shooping,
                    Comments = null,
                    PaidDate = DateTime.Now.AddDays(20),
                    QuoteAmount = 1000,
                    QuoteFee = 5,
                    QuoteNumber = 5,
                    PaidDestination = null,
                    PaidMethod = PaidMethodOperation.CreditCard
                }
            };
        }
    }
}
