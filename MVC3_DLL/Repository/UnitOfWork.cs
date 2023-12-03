using MVC_DAL.Contexts;
using MVC3_BLL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC3_BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext dbContext;

        public IEmployeeRepository EmployeeRepository { get; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public UnitOfWork(CompanyDbContext dbContext)
        {
                EmployeeRepository =new EmployeeRepository(dbContext);
                DepartmentRepository =new DepartmentRepository(dbContext);
            this.dbContext = dbContext;
        }



        public async Task<int> Complete()
        => await dbContext.SaveChangesAsync();

        public void Dispose()
        =>dbContext.Dispose();
    }
}
