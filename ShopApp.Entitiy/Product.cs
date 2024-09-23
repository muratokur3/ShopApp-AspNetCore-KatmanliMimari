using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Entitiy
{
    public class Product
    {
        public int ProductId { get; set; }
       public string Name { get; set; }
       public decimal Price { get; set; }
        public string Description { get; set; }
        public bool isApproved { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        
    }
}
