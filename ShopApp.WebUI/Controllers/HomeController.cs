using Microsoft.AspNetCore.Mvc;
using ShopApp.DataAccess.Abstract;
using ShopApp.WebUI.ViewModels;
using System.Diagnostics;

namespace ShopApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
       
        private IProductRepository _productRepository;
        public HomeController(IProductRepository productRepository)
        {
           this._productRepository = productRepository;
        }

        public IActionResult Index()
        {

            var productViewModel = new ProductViewModel
            {
                Products = _productRepository.GetAll()
            };

            return View(productViewModel);
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }

        
        
    }
}
