using DomainModel;
using DomainModel.Operation;
using DomainModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructureDB.Repository
{
    public interface IOperationRepository : IRepositoryBase<Operation>
    {
        public List<Operation> GetOperationsOfLastDays(int userId, DateTime daysToSearch);
        public List<Operation> GetOperationsOfLastDaysByStatus(int userId, DateTime daysToSearch, StatusOperation status);
        public List<Operation> GetOperationsOfLastDaysByField(int userId, DateTime daysToSearch, FieldOperation field);
        public List<Operation> GetOperationsOfLastDaysByPaidMethod(int userId, DateTime daysToSearch, PaidMethodOperation paidMethod);
    }
}
