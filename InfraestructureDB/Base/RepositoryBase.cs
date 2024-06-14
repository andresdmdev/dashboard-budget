using DomainModel;
using InfraestructureDB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructureDB.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DashboardDBContext dBContext { get; set; }
        public RepositoryBase(DashboardDBContext context)
        {
            this.dBContext = context;
        }
        public T Create(T entiyBase)
        {
            DateTime now = DateTime.Now;
            (entiyBase as EntityBase).CreatedDate = now;
            (entiyBase as EntityBase).UpdatedDate = now;
            (entiyBase as EntityBase).DeletedDate = null;
            (entiyBase as EntityBase).DeletedBy = null;

            try
            {
                dBContext.Set<T>().Add(entiyBase);

                dBContext.SaveChanges();
                return entiyBase;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create - {ex.Message}");
                return null;
            }
        }

        public T Update(T entiyBase, string updatedBy)
        {
            DateTime now = DateTime.Now;
            (entiyBase as EntityBase).UpdatedDate = now;
            (entiyBase as EntityBase).UpdatedBy = updatedBy;

            try
            {
                dBContext.Set<T>().Update(entiyBase);

                dBContext.SaveChanges();
                return entiyBase;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update - {ex.Message}");
                return null;
            }
        }

        public bool Delete(T entiyBase, string deletedBy)
        {
            DateTime now = DateTime.Now;
            (entiyBase as EntityBase).DeletedDate = now;
            (entiyBase as EntityBase).DeletedBy = deletedBy;

            try
            {
                dBContext.Set<T>().Update(entiyBase);

                dBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete - {ex.Message}");
                return false;
            }
        }

        public T Get(int entityId)
        {
            try
            {
                T? entity = dBContext.Set<T>().Find(entityId);

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get - {ex.Message}");
                return null;
            }
        }
    }
}
