namespace WebApp.Models
{
    public class CustomerWithSellerViewModel : CustomerViewModel
    {
        public CustomerWithSellerViewModel(Client client)
            : base(client)
        {
            Seller = client.Seller.Name;
        }

        public string Seller { get; set; }
    }
}