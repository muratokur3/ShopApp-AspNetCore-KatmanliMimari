﻿using Microsoft.Identity.Client;
using ShopApp.Business.Abstratc;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entity;
using System;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitofwork;
        public ProductManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public bool Create(Product entity)
        {
            if (Validate(entity))
            {
                _unitofwork.Products.Create(entity);
                _unitofwork.save();
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            _unitofwork.Products.Delete(entity);
            _unitofwork.save();
        }

        public List<Product> GetAll()
        {
            return _unitofwork.Products.GetAll();
        }

        public Product GetProductDetails(string url)
        {
            return _unitofwork.Products.GetProductDetails(url);
        }

        public Product GetById(int id)
        {
            return _unitofwork.Products.GetById(id);
        }

    

        public List<Product> GetProductByCategory(string name, int page, int pageSize)
        {
            return _unitofwork.Products.GetProductByCategory(name, page, pageSize);
        }

        public int GetCountByCategory(string category)
        {
            return _unitofwork.Products.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _unitofwork.Products.GetHomePageProducts();
        }

        public List<Product> GetSearchResult(string searchString)
        {
            return _unitofwork.Products.GetSearchResult(searchString);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _unitofwork.Products.GetByIdWithCategories(id);
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
            _unitofwork.Products.Update(entity);
            _unitofwork.save();
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
                _unitofwork.Products.Update(entity, categoryIds);
                _unitofwork.save();
                return true;
            }
            return false;
        }
    }
}
