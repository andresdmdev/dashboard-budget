using DomainModel.User;
using InfraestructureDB.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        public ServiceResponse<UserDashboard> CreateUserDashboard(UserDashboard user)
        {
            _logger.LogInformation($"UserService | CreateUserDashboard | Start | User: {user.ToString()}");

            UserDashboard userDashboard = _userRepository.Create(user);

            if (UserDashboard.IsNullOrNew(userDashboard))
            {
                _logger.LogError($"UserService | CreateUserDashboard | Cannot create an user dashboard | User: {user.ToString()}");
                return ServiceResponse<UserDashboard>.CreateFailedResponse(user, "Cannot create an user", 400);
            }

            _logger.LogInformation($"UserService | CreateUserDashboard | End | User: {userDashboard.ToString()}");

            return ServiceResponse<UserDashboard>.CreateSucessResponse(userDashboard, "User created sucessfuly", 200);
        }

        public ServiceResponse<bool> DeleteUserDashboard(int userId, string deleteBy)
        {
            _logger.LogInformation($"UserService | DeleteUserDashboard | Start | UserId: {userId.ToString()} | DeleteBy: {deleteBy}");

            if (userId > 0)
            {
                UserDashboard userDashboard = _userRepository.Get(userId);

                if (UserDashboard.IsNullOrNew(userDashboard))
                {
                    _logger.LogError($"UserService | DeleteUserDashboard | Cannot delete user dashboard | User: {userId.ToString()}");

                    return ServiceResponse<bool>.CreateFailedResponse(false);
                }

                _logger.LogError($"UserService | DeleteUserDashboard | User dashboard found | User: {userDashboard.ToString()}");

                bool response = _userRepository.Delete(userDashboard, deleteBy);

                return ServiceResponse<bool>.CreateSucessResponse(response);
            }
            else
            {
                _logger.LogError($"UserService | DeleteUserDashboard | User id is null or is not valid | User: {userId.ToString()}");

                return ServiceResponse<bool>.CreateFailedResponse(false);
            }
        }


        public ServiceResponse<UserDashboard> UpdateUserDashboard(UserDashboard user)
        {
            _logger.LogInformation($"UserService | UpdateUserDashboard | Start | User: {user.ToString()}");

            UserDashboard userDashboard = null;

            if (user.Id > 0)
            {
                userDashboard = _userRepository.Update(user);

                if (UserDashboard.IsNullOrNew(userDashboard))
                {
                    _logger.LogError($"UserService | UpdateUserDashboard | Cannot update user dashboard | User: {userDashboard.ToString()}");

                    return ServiceResponse<UserDashboard>.CreateFailedResponse(userDashboard);
                }
            }
            else
            {
                _logger.LogError($"UserService | UpdateUserDashboard | User id is null or is not valid | User: {user.ToString()}");

                return ServiceResponse<UserDashboard>.CreateFailedResponse(userDashboard);
            }
            
            _logger.LogInformation($"UserService | UpdateUserDashboard | End");

            return ServiceResponse<UserDashboard>.CreateSucessResponse(userDashboard);
        }

        public ServiceResponse<UserDashboard> GetUserDashboard(int userId)
        {
            _logger.LogInformation($"UserService | GeteUserDashboard | Start | UserId: {userId.ToString()}");

            UserDashboard userDashboard = null;

            if (userId > 0)
            {
                userDashboard = _userRepository.Get(userId);

                if (UserDashboard.IsNullOrNew(userDashboard) || !string.IsNullOrEmpty(userDashboard.DeletedBy) || !string.IsNullOrEmpty(userDashboard.DeletedDate.ToString()))
                {
                    _logger.LogError($"UserService | GeteUserDashboard | Cannot get user dashboard or was deleted | User: {userId.ToString()}");

                    return ServiceResponse<UserDashboard>.CreateFailedResponse(userDashboard);
                }
            }
            else
            {
                _logger.LogError($"UserService | GeteUserDashboard | User id is null or is not valid | User: {userId.ToString()}");

                return ServiceResponse<UserDashboard>.CreateFailedResponse(userDashboard);
            }

            _logger.LogInformation($"UserService | GeteUserDashboard | End");

            return ServiceResponse<UserDashboard>.CreateSucessResponse(userDashboard);
        }
    }
}
