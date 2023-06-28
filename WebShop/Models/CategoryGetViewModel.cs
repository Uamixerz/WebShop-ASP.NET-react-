namespace WebShop.Models
{
    public class CategoryGetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        
    }
}
