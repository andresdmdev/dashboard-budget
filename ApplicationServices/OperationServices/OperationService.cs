using DomainModel.Operation;
using DomainModel.User;
using InfraestructureDB.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.OperationServices
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _operationRepository;
        private readonly ILogger<OperationService> _logger;

        public OperationService(ILogger<OperationService> logger, IOperationRepository operationRepository)
        {
            _logger = logger;
            _operationRepository = operationRepository;
        }

        public ServiceResponse<Operation> CreateOperationDashboard(Operation operation)
        {
            _logger.LogInformation($"OperationService | CreateOperationDashboard | Start | Operation: {operation.ToString()}");

            Operation operationDashboard = _operationRepository.Create(operation);

            if (DomainModel.EntityBase.IsNullOrNew(operationDashboard))
            {
                _logger.LogError($"OperationService | CreateOperationDashboard | Cannot create an operation dashboard | Operation: {operationDashboard.ToString()}");
                return ServiceResponse<Operation>.CreateFailedResponse(operationDashboard, "Cannot create an operation", 400);
            }

            _logger.LogInformation($"OperationService | CreateOperationDashboard | End | Operation: {operationDashboard.ToString()}");

            return ServiceResponse<Operation>.CreateSucessResponse(operationDashboard, "Operation created sucessfuly", 200);
        }

        public ServiceResponse<bool> DeleteOperationDashboard(int operationId, string deleteBy)
        {
            _logger.LogInformation($"OperationService | DeleteOperationDashboard | Start | OperationId: {operationId.ToString()} | DeleteBy: {deleteBy}");

            if (operationId > 0)
            {
                Operation operationDashboard = _operationRepository.Get(operationId);

                if (Operation.IsNullOrNew(operationDashboard))
                {
                    _logger.LogError($"OperationService | DeleteOperationDashboard | Cannot delete operation dashboard | Operation: {operationId.ToString()}");

                    return ServiceResponse<bool>.CreateFailedResponse(false);
                }

                _logger.LogInformation($"OperationService | DeleteOperationDashboard | Operation dashboard found | Operation: {operationDashboard.ToString()}");

                bool response = _operationRepository.Delete(operationDashboard, deleteBy);

                return ServiceResponse<bool>.CreateSucessResponse(response);
            }
            else
            {
                _logger.LogError($"OperationService | DeleteOperationDashboard | Operation id is null or is not valid | Operation: {operationId.ToString()}");

                return ServiceResponse<bool>.CreateFailedResponse(false);
            }
        }

        public ServiceResponse<Operation> GetOperationDashboard(int operationId)
        {
            _logger.LogInformation($"OperationService | GetOperationDashboard | Start | OperationId: {operationId.ToString()}");

            Operation operationDashboard = default(Operation);

            if (operationId > 0)
            {
                operationDashboard = _operationRepository.Get(operationId);

                if (Operation.IsNullOrNew(operationDashboard) || !string.IsNullOrEmpty(operationDashboard.DeletedBy) || !string.IsNullOrEmpty(operationDashboard.DeletedDate.ToString()))
                {
                    _logger.LogError($"OperationService | GetOperationDashboard | Cannot get operation dashboard or was deleted | Operation: {operationId.ToString()}");

                    return ServiceResponse<Operation>.CreateFailedResponse(operationDashboard);
                }
            }
            else
            {
                _logger.LogError($"OperationService | GetOperationDashboard | Operation id is null or is not valid | Operation: {operationId.ToString()}");

                return ServiceResponse<Operation>.CreateFailedResponse(operationDashboard);
            }

            _logger.LogInformation($"OperationService | GetOperationDashboard | End");

            return ServiceResponse<Operation>.CreateSucessResponse(operationDashboard);
        }

        public ServiceResponse<List<Operation>> GetOperationsOfLastDays(int userId, int daysToSearch)
        {
            _logger.LogInformation($"OperationService | GetOperationsOfLastDays | Start | UserId: {userId}");

            List<Operation> operations = new List<Operation>();

            if (userId > 0)
            {
                int defaultDaysToSearch = daysToSearch == 0 ? 1 : daysToSearch;
                DateTime dateTimeToSearch = DateTime.Now.AddDays(-defaultDaysToSearch);

                operations = _operationRepository.GetOperationsOfLastDays(userId, dateTimeToSearch);
            }
            else {
                _logger.LogInformation($"OperationService | GetOperationsOfLastDays | UserId: {userId}");

                return ServiceResponse<List<Operation>>.CreateFailedResponse(operations);
            }

            _logger.LogInformation($"OperationService | GetOperationsOfLastDays | End | UserId: {userId}");

            return ServiceResponse<List<Operation>>.CreateSucessResponse(operations);
        }

        public ServiceResponse<List<Operation>> GetOperationsOfLastDays(int userId, DateTime daysToSearch)
        {
            _logger.LogInformation($"OperationService | GetOperationsOfLastDays | Start | UserId: {userId}");

            List<Operation> operations = new List<Operation>();

            if (userId > 0)
            {
                operations = _operationRepository.GetOperationsOfLastDays(userId, daysToSearch);
            }
            else
            {
                _logger.LogInformation($"OperationService | GetOperationsOfLastDays | UserId: {userId}");

                return ServiceResponse<List<Operation>>.CreateFailedResponse(operations);
            }

            _logger.LogInformation($"OperationService | GetOperationsOfLastDays | End | UserId: {userId}");

            return ServiceResponse<List<Operation>>.CreateSucessResponse(operations);
        }

        public ServiceResponse<List<Operation>> GetOperationsOfLastDaysByField(int userId, DateTime daysToSearch, FieldOperation field)
        {
            _logger.LogInformation($"OperationService | GetOperationsOfLastDaysByField | Start | UserId: {userId}");

            List<Operation> operations = new List<Operation>();

            if (userId > 0)
            {
                operations = _operationRepository.GetOperationsOfLastDaysByField(userId, daysToSearch, field);
            }
            else
            {
                _logger.LogInformation($"OperationService | GetOperationsOfLastDaysByField | UserId: {userId}");

                return ServiceResponse<List<Operation>>.CreateFailedResponse(operations);
            }

            _logger.LogInformation($"OperationService | GetOperationsOfLastDaysByField | End | UserId: {userId}");

            return ServiceResponse<List<Operation>>.CreateSucessResponse(operations);
        }

        public ServiceResponse<List<Operation>> GetOperationsOfLastDaysByPaidMethod(int userId, DateTime daysToSearch, PaidMethodOperation paidMethod)
        {
            _logger.LogInformation($"OperationService | GetOperationsOfLastDaysByPaidMethod | Start | UserId: {userId}");

            List<Operation> operations = new List<Operation>();

            if (userId > 0)
            {
                operations = _operationRepository.GetOperationsOfLastDaysByPaidMethod(userId, daysToSearch, paidMethod);
            }
            else
            {
                _logger.LogInformation($"OperationService | GetOperationsOfLastDaysByPaidMethod | UserId: {userId}");

                return ServiceResponse<List<Operation>>.CreateFailedResponse(operations);
            }

            _logger.LogInformation($"OperationService | GetOperationsOfLastDaysByPaidMethod | End | UserId: {userId}");

            return ServiceResponse<List<Operation>>.CreateSucessResponse(operations);
        }

        public ServiceResponse<List<Operation>> GetOperationsOfLastDaysByStatus(int userId, DateTime daysToSearch, StatusOperation status)
        {
            _logger.LogInformation($"OperationService | GetOperationsOfLastDays | Start | UserId: {userId}");

            List<Operation> operations = new List<Operation>();

            if (userId > 0)
            {
                operations = _operationRepository.GetOperationsOfLastDaysByStatus(userId, daysToSearch, status);
            }
            else
            {
                _logger.LogInformation($"OperationService | GetOperationsOfLastDays | UserId: {userId}");

                return ServiceResponse<List<Operation>>.CreateFailedResponse(operations);
            }

            _logger.LogInformation($"OperationService | GetOperationsOfLastDays | End | UserId: {userId}");

            return ServiceResponse<List<Operation>>.CreateSucessResponse(operations);
        }

        public ServiceResponse<Operation> UpdateOperationDashboard(Operation operation)
        {
            _logger.LogInformation($"OperationService | UpdateOperationDashboard | Start | Operation: {operation.ToString()}");

            Operation operationDashboard = default(Operation);

            if (operation.Id > 0)
            {
                operationDashboard = _operationRepository.Update(operation);

                if (Operation.IsNullOrNew(operationDashboard))
                {
                    _logger.LogError($"OperationService | UpdateOperationDashboard | Cannot update Operation dashboard | Operation: {operationDashboard.ToString()}");

                    return ServiceResponse<Operation>.CreateFailedResponse(operationDashboard);
                }
            }
            else
            {
                _logger.LogError($"OperationService | UpdateOperationDashboard | Operation id is null or is not valid | Operation: {operation.ToString()}");

                return ServiceResponse<Operation>.CreateFailedResponse(operationDashboard);
            }

            _logger.LogInformation($"OperationService | UpdateOperationDashboard | End");

            return ServiceResponse<Operation>.CreateSucessResponse(operationDashboard);
        }
    }
}
