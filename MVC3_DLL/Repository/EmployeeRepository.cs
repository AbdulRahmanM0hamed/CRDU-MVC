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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

       // private readonly CompanyDbContext _dbContext;

        public EmployeeRepository(CompanyDbContext dbContext) : base(dbContext)
        {
           // this._dbContext = dbContext;
        }



        IQueryable<Employee> IEmployeeRepository.GetEmployeeByAddress(string address)
         => dbContext.Employees.Where(E => E.Address == address);



        IQueryable<Employee> IEmployeeRepository.Search(string Name)
        => dbContext.Employees.Where(E => E.Name.Trim().ToLower().Contains(Name.Trim().ToLower()));


    }


}
