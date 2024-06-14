using DomainModel.User;
using InfraestructureDB.Base;
using InfraestructureDB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructureDB.Repository
{
    public class UserRepository : RepositoryBase<UserDashboard>, IUserRepository
    {
        public UserRepository(DashboardDBContext context) : base(context)
        {
        }
    }
}
