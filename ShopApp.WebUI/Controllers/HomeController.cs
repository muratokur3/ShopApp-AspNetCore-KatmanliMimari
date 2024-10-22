using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.DataAccess.Abstract;
using ShopApp.WebUI.Models;
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

        public async Task<IActionResult> Index()
        {
           var prducts= await _productServices.GetAll();
            var productViewModel = new ProductListViewModel
            {
                Products = prducts
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
