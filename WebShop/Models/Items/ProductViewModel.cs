namespace WebShop.Models.Items
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public List<int> ImagesID { get; set; }
        public string Description { get; set; }
        public int categoryId { get; set; }
    }
    public class ProductGetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<ProductImageItemViewModel> Images { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int categoryId { get; set; }
    }
    public class ProductGetHomePageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<ProductImageItemViewModel> Images { get; set; }
        public string Description { get; set; }
        public int? OldPrice { get; set; }
        public int categoryId { get; set; }
    }
    public class RelatedProductViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
    }
}
