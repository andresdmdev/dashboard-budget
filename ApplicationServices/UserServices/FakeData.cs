using DomainModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.UserServices
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
    }
}
