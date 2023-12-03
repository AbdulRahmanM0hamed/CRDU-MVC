using MVC_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC3_BLL.interfaces
{
    public interface IGenericRepository<T>
    {
       Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);

        Task Add(T item);
        void UpDate(T item);
        void Delete(T item);
    }
}
