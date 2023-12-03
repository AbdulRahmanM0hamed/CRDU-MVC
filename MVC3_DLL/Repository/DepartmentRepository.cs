using MVC_DAL.Contexts;
using MVC_DAL.Models;
using MVC3_BLL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC3_BLL.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
     
        public DepartmentRepository(CompanyDbContext dbContext):base(dbContext)
        {
           
        }
    }
}
