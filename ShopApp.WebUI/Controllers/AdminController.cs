using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        public AdminController(IProductService productService)
        {
            this._productService = productService;
        }
        public IActionResult ProductList()
        {
          return View(new ProductListViewModel()
          {
              Products = _productService.GetAll()
          });
        }
    }
}
