using DomainModel.Operation;
using InfraestructureDB.Base;
using InfraestructureDB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructureDB.Repository
{
    public class OperationRepository : RepositoryBase<Operation>, IOperationRepository
    {
        protected DashboardDBContext dBContext { get; set; }
        public OperationRepository(DashboardDBContext context) : base(context)
        {
            this.dBContext = context;
        }

        public List<Operation> GetOperationsOfLastDays(int userId, DateTime daysToSearch)
        {
            List<Operation> operations = new List<Operation>();

            try
            {
                operations = dBContext.Set<Operation>().Where(op => op.UserId == userId && 
                    op.Date.Date >= daysToSearch.Date &&
                    op.DeletedDate == null &&
                    op.DeletedBy == null
                    ).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOperationsOfLastDays - {ex.Message}");
            }

            return operations;
        }
        public List<Operation> GetOperationsOfLastDaysByStatus(int userId, DateTime daysToSearch, StatusOperation status)
        {
            List<Operation> operations = new List<Operation>();

            try
            {
                operations = dBContext.Set<Operation>().Where(op => op.UserId == userId && 
                    op.Date.Date >= daysToSearch.Date &&
                    op.Status == status &&
                    op.DeletedDate == null &&
                    op.DeletedBy == null
                    ).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOperationsOfLastDaysByStatus - {ex.Message}");
            }

            return operations;
        }

        public List<Operation> GetOperationsOfLastDaysByField(int userId, DateTime daysToSearch, FieldOperation field)
        {
            List<Operation> operations = new List<Operation>();

            try
            {
                operations = dBContext.Set<Operation>().Where(op => op.UserId == userId &&
                    op.Date.Date >= daysToSearch.Date &&
                    op.Field == field &&
                    op.DeletedDate == null &&
                    op.DeletedBy == null
                    ).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOperationsOfLastDaysByField - {ex.Message}");
            }

            return operations;
        }

        public List<Operation> GetOperationsOfLastDaysByPaidMethod(int userId, DateTime daysToSearch, PaidMethodOperation paidMethod)
        {
            List<Operation> operations = new List<Operation>();

            try
            {
                operations = dBContext.Set<Operation>().Where(op => op.UserId == userId &&
                    op.Date.Date >= daysToSearch.Date &&
                    op.PaidMethod == paidMethod &&
                    op.DeletedDate == null &&
                    op.DeletedBy == null
                    ).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOperationsOfLastDaysByPaidMethod - {ex.Message}");
            }

            return operations;
        }
    }
}
