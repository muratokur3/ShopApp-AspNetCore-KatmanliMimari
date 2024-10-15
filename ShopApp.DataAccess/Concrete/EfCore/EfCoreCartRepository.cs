using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCartRepository : EfcoreGenericRepository<Cart, ShopContext>, ICartRepository
    {
        public void DeleteFromCart(int carId, int productId)
        {
            using(var context = new ShopContext())
            {
                var cmd = @"delete from CartItems where CartId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd, carId, productId);
            }
        }

        public Cart GetByUserId(string userId)
        {
            using(var context = new ShopContext())
            {
                return context.Carts
                    .Include(i => i.CartItems)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault(i => i.UserId == userId);
            }
        }
        public override void Update(Cart entity)
        {
            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
