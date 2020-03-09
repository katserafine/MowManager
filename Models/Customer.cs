namespace MowManager.Models
{
    public class Customer
    {
        // Unique ID to reference instance
        public int ID { get; set; }

        // Active Customer?
        public bool Active { get; set; }

        // Service
        public Service Service { get; set; }

        // Mailing Address
        public string MailingAddress { get; set; }

        // Billing Address
        public string BillingAddress { get; set; }

        // Customer Email 
        public string Email { get; set; }

        // Customer that referred this Customer instance
        public Customer ReferredBy { get; set; }

        // Gate?
        public bool Gate { get; set; }

        // Community Gate?
        public bool CommunityGate { get; set; }

        // Lot size in square ft
        public int LotSize { get; set; }

        // Corner lot?
        public bool CornerLot { get; set; }

        // Follow Up?
        public bool FollowUp { get; set; }

        // Source
        // Mapsco

    }
}