using DomainModel;
using DomainModel.Operation;
using DomainModel.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructureDB.Context
{
    public class DashboardDBContext : DbContext
    {
        public DashboardDBContext() { }
        public DashboardDBContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<UserDashboard> UserDashboard { get; set; }
        public virtual DbSet<Operation> OperationDashboard { get; set; }
    }
}
