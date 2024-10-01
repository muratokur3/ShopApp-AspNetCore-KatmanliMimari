using ShopApp.Entity;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public string ImageUrl { get; set; }

        public List<Category> SelectedCategories { get; set; }
    }
}
