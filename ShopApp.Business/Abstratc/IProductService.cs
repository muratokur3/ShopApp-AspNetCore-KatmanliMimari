using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstratc
{
    public interface IProductService:IValidator<Product>
    {
        Product GetProductDetails(string url);

        Product GetByIdWithCategories(int id);
        List<Product> GetProductByCategory(string name, int page, int pageSize);
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResult(string searchString);
        Product GetById(int id);
        List<Product> GetAll();
        bool Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        int GetCountByCategory(string category);
        bool Update(Product entity, int[] categoryIds);
    }
}
