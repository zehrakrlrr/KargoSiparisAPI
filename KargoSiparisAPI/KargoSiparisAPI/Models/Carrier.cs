namespace KargoSiparisAPI.Models
{
    public class Carrier
    {
        public int CarrierId { get; set; }
        public string Name { get; set; }
        public int MinDesi { get; set; }
        public int MaxDesi { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerExtraDesi { get; set; }

       
        public List<Order> Orders { get; set; }
    }
}