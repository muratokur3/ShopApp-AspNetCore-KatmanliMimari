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

        List<Product> GetProductByCategory(string name,int page,int pageSize);
        Product GetProductDetails(string url);
        int GetCountByCategory(string category);

    }
}
