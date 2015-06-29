namespace WebApp.Models
{
    public class User : AbstractModel
    {
        public static readonly string ADMIN_ID = "682ca6cd-6502-4ae3-bf45-5153beb9c0a0";

        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsAdmin { get { return ID.ToString() == ADMIN_ID; } }
    }
}