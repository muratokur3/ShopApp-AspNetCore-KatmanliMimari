using Microsoft.EntityFrameworkCore;
using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId = 1, Name = "Telefon", Url = "telefon" },
                new Category() { CategoryId = 2, Name = "Bilgisayar", Url = "bilgisayar" },
                new Category() { CategoryId = 3, Name = "Tablet", Url = "tablet" },
                new Category() { CategoryId = 4, Name = "Saat", Url = "saat" },
                new Category() { CategoryId = 5, Name = "TV", Url = "tv" },
                new Category() { CategoryId = 6, Name = "Beyaz Eşya", Url = "beyaz-esya" }
            
            );

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, Name = "Iphone SE", Url = "iphone-se", Description = "iyi telefon", Price = 2000, IsApproved = true, IsHome = true, ImageUrl = "1.jpg" },
            new Product() { ProductId = 2, Name = "Iphone 6", Url = "iphone-6", Description = "güzel telefon", Price = 3000, IsApproved = true, IsHome = false, ImageUrl = "23.jpg" },
            new Product() { ProductId = 3, Name = "Iphone X", Url = "iphone-x", Description = "hızlı telefon", Price = 4000, IsApproved = true, IsHome = false, ImageUrl = "14.jpg" },
            new Product() { ProductId = 4, Name = "Iphone 16", Url = "iphone-16", Description = "Seri telefon", Price = 5000, IsApproved = true, IsHome = false, ImageUrl = "22.jpg" },
            new Product() { ProductId = 5, Name = "Iphone 13", Url = "iphone-13", Description = "minik telefon", Price = 6000, IsApproved = true, IsHome = false, ImageUrl = "13.jpg" },
            new Product() { ProductId = 6, Name = "Macbook Pro", Url = "macbook-pro", Description = "Güçlü Bilgisayar", Price = 4000, IsApproved = true, IsHome = true, ImageUrl = "4.jpg" },
            new Product() { ProductId = 7, Name = "Macbook Air", Url = "macbook-air", Description = "Hafif Bilgisayar", Price = 9000, IsApproved = true, IsHome = false, ImageUrl = "3.jpg" },
            new Product() { ProductId = 8, Name = "Ipad Air", Url = "ipad-air", Description = "minik Tablet", Price = 1000, IsApproved = true, IsHome = true, ImageUrl = "7.jpg" },
            new Product() { ProductId = 9, Name = "Apple Watch", Url = "apple-watch", Description = "kullanisli Saat", Price = 8000, IsApproved = true, IsHome = false, ImageUrl = "10.jpg" },
            new Product() { ProductId = 10, Name = "Apple TV", Url = "apple-tv", Description = "Eglenceli", Price = 6000, IsApproved = true, IsHome = false, ImageUrl = "11.jpg" },
            new Product() { ProductId = 11, Name = "Beko Buzdolabı", Url = "beko-buzdolabi", Description = "saglam", Price = 14000, IsApproved = true, IsHome = true, ImageUrl = "27.jpg" },
            new Product() { ProductId = 12, Name = "Arçelik Buzdolabı", Url = "arcelik-buzdolabi", Description = "kaliteli", Price = 16000, IsApproved = true, IsHome = false, ImageUrl = "28.jpg" },
            new Product() { ProductId = 13, Name = "Vestel Televizyon", Url = "vestel-televizyon", Description = "büyük", Price = 5000, IsApproved = true, IsHome = true, ImageUrl = "29.jpg" },
            new Product() { ProductId = 14, Name = "Asus Laptop", Url = "asus-laptop", Description = "hızlı", Price = 7000, IsApproved = true, IsHome = false, ImageUrl = "30.jpg" },
            new Product() { ProductId = 15, Name = "Arçelik Çamaşır Makinesi", Url = "arcelik-camasir-makinesi", Description = "kaliteli", Price = 12000, IsApproved = true, IsHome = false, ImageUrl = "31.jpg" }

            );
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory()
                {
                    CategoryId = 1,
                    ProductId = 1
                },
                new ProductCategory()
                {
                    CategoryId = 1,
                    ProductId = 2
                },
                new ProductCategory()
                {
                    CategoryId = 2,
                    ProductId = 3
                },
                new ProductCategory()
                {
                    CategoryId = 2,
                    ProductId = 4
                },
                new ProductCategory()
                {
                    CategoryId = 3,
                    ProductId = 5
                },
                new ProductCategory()
                {
                    CategoryId = 3,
                    ProductId = 6
                },
                new ProductCategory()
                {
                    CategoryId = 4,
                    ProductId = 7
                },
                new ProductCategory()
                {
                    CategoryId = 4,
                    ProductId = 8
                },
                new ProductCategory()
                {
                    CategoryId = 5,
                    ProductId = 9
                },
                new ProductCategory()
                {
                    CategoryId = 5,
                    ProductId = 10
                },
                new ProductCategory()
                {
                    CategoryId = 6,
                    ProductId = 11
                },
                new ProductCategory()
                {
                    CategoryId = 6,
                    ProductId = 12
                }

            );
        }
    }
}
