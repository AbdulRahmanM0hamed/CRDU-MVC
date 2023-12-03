using Microsoft.EntityFrameworkCore;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private protected readonly CompanyDbContext dbContext;

        public GenericRepository(CompanyDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


       

        public async Task< IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
              return  (IEnumerable<T>)await dbContext.Employees.Include(E => E.Department).ToListAsync();

            return await dbContext.Set<T>().ToListAsync();

        }


        public async Task<T> Get(int id)
        =>await dbContext.Set<T>().FindAsync(id);

        public async Task Add(T item)
         =>await dbContext.AddAsync(item);

        public void Delete(T item)
        => dbContext.Remove(item);


        public void UpDate(T item)
        =>  dbContext.Update(item);
          
    }
}
