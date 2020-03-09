namespace MowManager.Models
{
    public class Pricing
    {
        // Unique ID to reference instance
        public int ID { get; set; }

        // Name for the pricing rates
        public string Name { get; set; }

        // Dollar rate for a particular service
        public decimal Rate { get; set; }

        // Dollar rate with tax included for a particular service
        public decimal RateTaxIncluded { get; set; }

        // Tax value rate
        public decimal TaxValue { get; set; }
    }
}