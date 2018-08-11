namespace ExpressShop.Storage.Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Characteristics { get; set; }
        public decimal Price { get; set; }
        public int TotalQuantity { get; set; }
    }
}
