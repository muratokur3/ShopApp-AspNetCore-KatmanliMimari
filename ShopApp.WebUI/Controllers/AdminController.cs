using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.Business.Abstratc;
using ShopApp.Entity;
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

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Url = model.Url,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };
            _productService.Create(entity);
            var msg =  new AlertMessage()
            {
                AlertType = "success",
                Message = $"{entity.Name} was added successfully"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("productList");
        }

        public IActionResult EditProduct(int? id)
            {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _productService.GetById((int)id);
            if (entity == null) {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductModel model)
        {
            var entity = _productService.GetById(model.ProductId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Url = model.Url;
            entity.Price = model.Price;
            entity.Description = model.Description;
            entity.ImageUrl = model.ImageUrl;

            _productService.Update(entity);

            var msg =  new AlertMessage()
            {
                AlertType = "warning",
                Message = $"{entity.Name} was update successfully"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }
            var msg=new AlertMessage()
            {
                AlertType = "danger",
                Message = $"{entity.Name} was deleted successfully"
                };
            TempData["message"] =JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }

    }
}
