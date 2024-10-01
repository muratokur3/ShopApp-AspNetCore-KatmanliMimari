using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        List<Product> GetSearchResult(string searchString);

        List<Product> GetHomePageProducts();

        Product GetByIdWithCategories(int id);

        List<Product> GetProductByCategory(string name,int page,int pageSize);
        Product GetProductDetails(string url);

        void Update(Product entity, int[] categoryIds);
        int GetCountByCategory(string category);

    }
}
