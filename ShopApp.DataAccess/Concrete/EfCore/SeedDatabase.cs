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
                new Category() { Name = "Telefon",Url="telefon" },
                new Category() { Name = "Bilgisayar",Url= "bilgisayar" },
                new Category() { Name = "Tablet",Url= "tablet" },
                new Category() { Name = "Saat",Url="saat" },
                new Category() { Name = "TV",Url= "tv" },
                new Category() { Name = "Beyaz Eşya",Url= "beyaz-esya" }
            };

        private static Product[] Products = {
            new Product() { Name = "Iphone SE",Url="iphone-se",Description="iyi telefon", Price = 2000,IsApproved=true,IsHome=true, ImageUrl = "1.jpg" },
            new Product() { Name = "Iphone 6",Url="iphone-6", Description="güzel telefon",Price = 3000,IsApproved=true,IsHome=false, ImageUrl = "23.jpg" },
            new Product() { Name = "Iphone X",Url="iphone-x", Description="hızlı telefon",Price = 4000,IsApproved=true,IsHome=false, ImageUrl = "14.jpg" },
            new Product() { Name = "Iphone 16",Url="iphone-16",Description="Seri telefon", Price = 5000,IsApproved=true,IsHome=false, ImageUrl = "22.jpg" },
            new Product() { Name = "Iphone 13",Url="iphone-13",Description="minik telefon", Price = 6000,IsApproved=true,IsHome=false, ImageUrl = "13.jpg" },
            new Product() { Name = "Macbook Pro",Url="macbook-pro",Description="Güçlü Bilgisayar", Price = 4000,IsApproved=true,IsHome=true, ImageUrl = "4.jpg" },
            new Product() { Name = "Macbook Air",Url="macbook-air",Description="Hafif Bilgisayar", Price = 9000,IsApproved=true,IsHome=false, ImageUrl = "3.jpg" },
            new Product() { Name = "Ipad Air",Url="ipad-air",Description="minik Tablet", Price = 1000,IsApproved=true,IsHome=true, ImageUrl = "7.jpg" },
            new Product() { Name = "Apple Watch",Url="apple-watch",Description="kullanisli Saat", Price = 8000,IsApproved=true,IsHome=false, ImageUrl = "10.jpg" },
            new Product() { Name = "Apple TV",Url="apple-tv",Description="Eglenceli", Price = 6000,IsApproved=true,IsHome=false, ImageUrl = "11.jpg" },
            new Product() { Name = "Beko Buzdolabı",Url="beko-buzdolabi",Description="saglam", Price = 14000,IsApproved=true,IsHome=true, ImageUrl = "27.jpg" },
            new Product() { Name = "Arçelik Buzdolabı",Url="arcelik-buzdolabi",Description="kaliteli", Price = 16000,IsApproved=true,IsHome=false, ImageUrl = "28.jpg" },
            new Product() { Name = "Vestel Televizyon",Url="vestel-televizyon",Description="büyük", Price = 5000,IsApproved=true,IsHome=true, ImageUrl = "29.jpg" },
            new Product() { Name = "Asus Laptop",Url="asus-laptop",Description="hızlı", Price = 7000,IsApproved=true,IsHome=false, ImageUrl = "30.jpg" },
            new Product() { Name = "Arçelik Çamaşır Makinesi",Url="arcelik-camasir-makinesi",Description="kaliteli", Price = 12000,IsApproved=true,IsHome=false, ImageUrl = "31.jpg" },
     


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
