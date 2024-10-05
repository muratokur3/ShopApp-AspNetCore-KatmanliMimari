using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstratc
{
    public interface ICategoryService:IValidator<Category>
    {
        Category GetByIdWithProducts(int categoryId);
        Category GetById(int id);
        void DeleteFromCategory(int productId, int categoryId);
        List<Category> GetAll();
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
       
    }
}
