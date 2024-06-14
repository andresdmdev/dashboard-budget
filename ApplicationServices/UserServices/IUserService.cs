using DomainModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.UserServices
{
    public interface IUserService
    {
        ServiceResponse<UserDashboard> CreateUserDashboard(UserDashboard user);
        ServiceResponse<UserDashboard> UpdateUserDashboard(UserDashboard userDashboard);
        ServiceResponse<UserDashboard> GetUserDashboard(int userId);
        ServiceResponse<bool> DeleteUserDashboard(int userId, string deleteBy);
    }
}
