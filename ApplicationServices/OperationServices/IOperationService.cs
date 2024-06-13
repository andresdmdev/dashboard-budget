using DomainModel.Operation;
using DomainModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.OperationServices
{
    public interface IOperationService
    {
        ServiceResponse<Operation> CreateOperationDashboard(Operation operation);
        ServiceResponse<Operation> UpdateOperationDashboard(Operation operation);
        ServiceResponse<Operation> GetOperationDashboard(int operationId);
        ServiceResponse<bool> DeleteOperationDashboard(int operationId, string deleteBy);
        ServiceResponse<List<Operation>> GetOperationsOfLastDays(int userId, int daysToSearch);
        ServiceResponse<List<Operation>> GetOperationsOfLastDays(int userId, DateTime daysToSearch);
        ServiceResponse<List<Operation>> GetOperationsOfLastDaysByStatus(int userId, DateTime daysToSearch, StatusOperation status);
        ServiceResponse<List<Operation>> GetOperationsOfLastDaysByField(int userId, DateTime daysToSearch, FieldOperation field);
        ServiceResponse<List<Operation>> GetOperationsOfLastDaysByPaidMethod(int userId, DateTime daysToSearch, PaidMethodOperation paidMethod);
    }
}
