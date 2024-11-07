using System.Threading;

namespace KargoSiparisAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Desi { get; set; }
        public int CarrierId { get; set; }
        public decimal ShippingPrice { get; set; }
        public DateTime OrderDate { get; set; }

       
        public Carrier Carrier { get; set; }
    }
}
