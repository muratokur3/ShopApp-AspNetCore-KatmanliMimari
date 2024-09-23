using Microsoft.EntityFrameworkCore;
using ShopApp.Entity;


namespace ShopApp.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {

        public static void Seed()
        {
            var context = new ShopContext();

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }

                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategories);
                }
            }
            context.SaveChanges();
        }

        private static Category[] Categories =
              {
                new Category() { Name = "Telefon" },
                new Category() { Name = "Bilgisayar" },
                new Category() { Name = "Tablet" },
                new Category() { Name = "Saat" },
                new Category() { Name = "TV" }
            };

        private static Product[] Products = {
            new Product() { Name = "Iphone SE",Description="iyi telefon", Price = 2000,IsApproved=true, ImageUrl = "1.jpg" },
            new Product() { Name = "Iphone 6", Description="güzel telefon",Price = 3000,IsApproved=true, ImageUrl = "2.jpg" },
            new Product() { Name = "Iphone X", Description="hızlı telefon",Price = 4000,IsApproved=true, ImageUrl = "3.jpg" },
            new Product() { Name = "Iphone 16",Description="Seri telefon", Price = 5000,IsApproved=true, ImageUrl = "4.jpg" },
            new Product() { Name = "Iphone 13",Description="minik telefon", Price = 6000,IsApproved=true, ImageUrl = "11.jpg" },
            new Product() { Name = "Macbook Pro",Description="Güçlü Bilgisayar", Price = 6000,IsApproved=true, ImageUrl = "21.jpg" },
            new Product() { Name = "Macbook Air",Description="HAfif Bilgisayar", Price = 6000,IsApproved=true, ImageUrl = "20.jpg" },
            new Product() { Name = "Ipad Air",Description="minik Tablet", Price = 6000,IsApproved=true, ImageUrl = "12.jpg" },
            new Product() { Name = "Apple Watch",Description="kullanisli Saat", Price = 6000,IsApproved=true, ImageUrl = "7.jpg" },
            new Product() { Name = "Apple TV",Description="Eglenceli", Price = 6000,IsApproved=true, ImageUrl = "17.jpg" }


        };

        private static ProductCategory[] ProductCategories =
        {
            new ProductCategory() { Product = Products[0], Category = Categories[0] },
            new ProductCategory() { Product = Products[0], Category = Categories[1] },
            new ProductCategory() { Product = Products[1], Category = Categories[0] },
            new ProductCategory() { Product = Products[1], Category = Categories[1] },
            new ProductCategory() { Product = Products[1], Category = Categories[2] },
            new ProductCategory() { Product = Products[2], Category = Categories[0] },
            new ProductCategory() { Product = Products[2], Category = Categories[1] },
            new ProductCategory() { Product = Products[2], Category = Categories[2] },
            new ProductCategory() { Product = Products[3], Category = Categories[0] },
            new ProductCategory() { Product = Products[3], Category = Categories[1] },
            new ProductCategory() { Product = Products[3], Category = Categories[2] },
            new ProductCategory() { Product = Products[4], Category = Categories[0] },
            new ProductCategory() { Product = Products[4], Category = Categories[1] },
            new ProductCategory() { Product = Products[4], Category = Categories[2] },
            new ProductCategory() { Product = Products[5], Category = Categories[1] },
            new ProductCategory() { Product = Products[5], Category = Categories[2] },
            new ProductCategory() { Product = Products[6], Category = Categories[1] },
            new ProductCategory() { Product = Products[7], Category = Categories[2] },
            new ProductCategory() { Product = Products[8], Category = Categories[3] },
            new ProductCategory() { Product = Products[9], Category = Categories[4] }

    };

    };
}
