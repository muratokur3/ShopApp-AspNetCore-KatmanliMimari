using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
