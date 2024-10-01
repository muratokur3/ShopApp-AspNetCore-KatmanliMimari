using Azure;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreProductRepository : EfcoreGenericRepository<Product, ShopContext>, IProductRepository
    {
        public Product GetByIdWithCategories(int id)
        {
            using (var context = new ShopContext())
            {

                return context.Products
                                 .Where(i => i.ProductId == id)
                                 .Include(i => i.ProductCategories)
                                 .ThenInclude(i => i.Category)
                                 .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.Where(i => i.IsApproved).AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.ProductCategories.Any(a => a.Category.Url == category));
                }
                return products.Count();
            }
        }

        public List<Product> GetHomePageProducts()
        {
            using (var context = new ShopContext())
            {
                return context.Products.Where(i => i.IsHome).ToList();
            }
        }

        public List<Product> GetProductByCategory(string name, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.ProductCategories.Any(a => a.Category.Url == name));
                }
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public Product GetProductDetails(string url)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                                .Where(i => i.Url == url)
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .FirstOrDefault();

            }
        }

        public List<Product> GetSearchResult(string searchString)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(searchString))
                {
                    products = products.Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())));
                }
                return products.ToList();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products
                                    .Include(i => i.ProductCategories)
                                    .FirstOrDefault(i => i.ProductId == entity.ProductId);
                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Url = entity.Url;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    product.ImageUrl = entity.ImageUrl;
                    product.IsApproved = entity.IsApproved;
                    product.IsHome = entity.IsHome;
                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        CategoryId = catid,
                        ProductId = entity.ProductId
                    }).ToList();
                    context.SaveChanges();
                }
            }
        }
    }
}
