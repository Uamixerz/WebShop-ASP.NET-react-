namespace WebShop.Models.Orders
{
    public class OrderViewModel
    {
        public string PhoneNumber { get; set; }
        public int userId { get; set; }
        public int orderStatusId { get; set; }
        public int payMethodId { get; set; }
        public int payStatusId { get; set; }
        public int postOfficeId { get; set; }

    }
}
