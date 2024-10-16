using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface ICartRepository: IRepository<Cart>
    {
        void DeleteFromCart(int carId, int productId);
        Cart GetByUserId(string userId);
        void ClearCart(int cartId);
    }
}
