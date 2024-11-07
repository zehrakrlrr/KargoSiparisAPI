namespace KargoSiparisAPI.Models
{
    public class CarrierConfiguration
    {
        public int CarrierConfigurationId { get; set; }
        public int CarrierId { get; set; }
        public string ConfigDetails { get; set; }

       
        public Carrier Carrier { get; set; }
    }
}
