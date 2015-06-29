namespace WebApp.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel(Client client)
        {
            Classification = client.Classification.Name;
            Name = client.Name;
            Phone = client.Phone;
            Gender = client.Gender;
            City = client.Region.City.Name;
            Region = client.Region.Name;
            LastPurchase = client.LastPurchase.ToString("yyyy/MM/dd");
        }

        public string Classification { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string LastPurchase { get; set; }
    }
}