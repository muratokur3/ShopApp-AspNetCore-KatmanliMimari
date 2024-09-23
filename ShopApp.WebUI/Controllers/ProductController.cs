using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ShopApp.Entity;
using ShopApp.WebUI.ViewModels;

namespace ShopApp.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var p = new Product();
            p.Name = "Samsung S1";
            p.Price = 5000;
            p.Description = "güzel telefon";
            ViewBag.Category = "Telefon";
            ViewBag.Product = p;
            return View();
        }

        public IActionResult List(int? id,string q)
        {
            //var products = ProductRepository.Products;

            //if (id != null)
            //{
            //    products = products.Where(p => p.CategoryId == id).ToList();
            //}
            //if(!string.IsNullOrEmpty(q))
            //{
            //    products = products.Where(p => p.Name.ToLower().Contains(q.ToLower())||p.Description.Contains(q)).ToList();
            //}

            //var productViewModel = new ProductViewModel
            //{
            //    Products = products

            //};

            //return View(productViewModel);
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();

        }
        public IActionResult Create()
        {

            return View(new Product());
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(p);
            //}
            //ProductRepository.AddProduct(p);
            //return RedirectToAction("List");
            return View();
        }

        public IActionResult Edit(int id)
        {

            //return View(ProductRepository.GetProductById(id));
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Product p)
        {
            //ProductRepository.EditProduct(p);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(int ProductId)
        {
            //ProductRepository.DeleteProduct(ProductId);
            return RedirectToAction("List");
        }
    }
}
