using DomainModel;
using DomainModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructureDB.Repository
{
    public interface IUserRepository : IRepositoryBase<UserDashboard>
    {
    }
}
