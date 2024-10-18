using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entity;


namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfcoreGenericRepository<Category >, ICategoryRepository
    {
        public EfCoreCategoryRepository(ShopContext context) : base(context)
        {
        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        public void DeleteFromCategory(int productId, int categoryId)
        {
                var cmd = @"delete from ProductCategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
        }

        public Category GetByIdWithProducts(int categoryId)
        {

                return ShopContext.Categories
                     .Where(i => i.CategoryId == categoryId)
                     .Include(i => i.ProductCategories)
                     .ThenInclude(i => i.Product)
                     .FirstOrDefault();
        }
    }
}
