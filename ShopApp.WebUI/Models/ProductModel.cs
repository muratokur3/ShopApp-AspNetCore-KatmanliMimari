using ShopApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [StringLength(60,MinimumLength =5,ErrorMessage ="Name must be between 5 and 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1,100000,ErrorMessage ="Price must be between 1 and 100000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Description must be between 20 and 500 characters")]
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        public string ImageUrl { get; set; }

        public List<Category> SelectedCategories { get; set; }=new List<Category>();
    }
}
