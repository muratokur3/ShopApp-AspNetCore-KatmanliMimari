using ShopApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category url is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category url must be between 2 and 100 characters")]
        public string Url { get; set; }
        public List<Product> Products { get; set; }
    }
}
