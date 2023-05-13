namespace WebShop.Models
{
    public class CategoryCreateViewModel
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
    }
}
