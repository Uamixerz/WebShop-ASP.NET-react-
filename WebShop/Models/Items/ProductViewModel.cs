namespace WebShop.Models.Items
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public List<int> ImagesID { get; set; }
        public string Description { get; set; }
        public int categoryId { get; set; }
    }
    public class ProductGetViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public List<ProductImageItemViewModel> Images { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int categoryId { get; set; }
    }
}
