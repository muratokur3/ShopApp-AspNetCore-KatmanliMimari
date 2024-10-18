using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private UserManager<User> _userManager;
        public OrderController(IOrderService orderService,UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
           var orders= _orderService.GetOrders(userId);
            var orderListModel =  orders.Select(i => new OrderListModel()
            {
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                UserId = i.UserId,
                FirstName = i.FirstName,
                LastName = i.LastName,
                Address = i.Address,
                City = i.City,
                Phone = i.Phone,
                Email = i.Email,
                Note = i.Note,
                OrderState = i.OrderState,
                OrderItems = i.OrderItems.Select(a => new OrderItemModel()
                {
                    OrderItemId = a.Id,
                    Price = (double)a.Price,
                    Name = a.Product.Name,
                    ImageUrl = a.Product.ImageUrl,
                    Quantity = a.Quantity
                }).ToList()
            }).ToList();
            return View("Index", orderListModel);
        }

     

       
    }
}
