using Microsoft.Identity.Client;
using ShopApp.Business.Abstratc;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entity;
using System;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;



        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool Create(Product entity)
        {
            if (Validate(entity))
            {
                _productRepository.Create(entity);
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetProductDetails(string url)
        {
            return _productRepository.GetProductDetails(url);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

    

        public List<Product> GetProductByCategory(string name, int page, int pageSize)
        {
            return _productRepository.GetProductByCategory(name, page, pageSize);
        }

        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _productRepository.GetHomePageProducts();
        }

        public List<Product> GetSearchResult(string searchString)
        {
            return _productRepository.GetSearchResult(searchString);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productRepository.GetByIdWithCategories(id);
        }

     

        public string ErrorMessage { get; set; }
        public bool Validate(Product entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "Ürün ismi girin. \n";
                isValid = false;
            }
            if (entity.Price<5)
            {
                ErrorMessage += "Fiyat negatif olamaz. \n";
                isValid = false;
            }
            return isValid;
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public bool Update(Product entity, int[] categoryIds)
        {
            if (Validate(entity))
            {
                if (categoryIds.Length == 0)
                {
                    ErrorMessage += "Ürün için en az bir kategori seçmelisiniz.";
                    return false;
                }
                _productRepository.Update(entity, categoryIds);
                return true;
            }
            return false;
        }
    }
}
