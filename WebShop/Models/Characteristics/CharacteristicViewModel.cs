namespace WebShop.Models.Characteristics
{
    public class CharacteristicViewModel
    {
        public string Name { get; set; }
        public List<int> CategoriesId { get; set; }
        
    }
    public class CharacteristicGetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryForCharacteristic> Categories { get; set; }

    }
    public class CategoryForCharacteristic
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CharacteristicEditViewModel
    {
        public int IdCharacteristic { get; set; }
        public string Name { get; set; }
        public List<int> CategoriesId { get; set; }

    }
}
