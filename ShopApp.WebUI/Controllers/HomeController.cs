using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.DataAccess.Abstract;
using ShopApp.WebUI.ViewModels;
using System.Diagnostics;

namespace ShopApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
       
        private IProductService _productServices;
        public HomeController(IProductService productServices)
        {
           this._productServices = productServices;
        }

        public IActionResult Index()
        {

            var productViewModel = new ProductListViewModel
            {
                Products = _productServices.GetAll()
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
