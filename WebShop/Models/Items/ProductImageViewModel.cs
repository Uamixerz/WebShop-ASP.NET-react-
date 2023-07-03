using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models.Items
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string UrlImage { get; set; }
        public int ProductId { get; set; }
    }
    public class ProductImageUploadViewModel
    {
        public IFormFile Image { get; set; }
    }
    public class ProductImageItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
