using ShopApp.Entity;


namespace ShopApp.DataAccess.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
      Category GetByIdWithProducts(int categoryId);

        void DeleteFromCategory(int productId, int categoryId);
    }


}
