using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public interface IRepositoryBase<T>
    {
        T Create(T entiyBase);
        T Update(T entiyBase, string updatedBy = "amarquez");
        bool Delete(T entiyBase, string deletedBy);
        T Get(int entityId);
    }
}
