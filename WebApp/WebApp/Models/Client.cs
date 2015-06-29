using System;

namespace WebApp.Models
{
    public class Client : AbstractModel
    {
        public string Phone { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string Occupation { get; set; }

        public DateTime LastPurchase { get; set; }

        public Guid RegionID { get; set; }

        public virtual Region Region { get; set; }

        public Guid SellerID { get; set; }

        public virtual User Seller { get; set; }

        public Guid ClassificationID { get; set; }

        public virtual Classification Classification { get; set; }
    }
}