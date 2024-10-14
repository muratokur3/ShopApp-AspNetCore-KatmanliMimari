using Microsoft.AspNetCore.Mvc;

namespace ShopApp.WebUI.Controllers
{
    public class CartController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart()
        {
            return View();
        }
    }
}
