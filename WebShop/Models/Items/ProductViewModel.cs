namespace WebShop.Models.Items
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
    }
}
