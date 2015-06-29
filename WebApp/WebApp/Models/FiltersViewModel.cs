using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public class FiltersViewModel
    {
        public FiltersViewModel(WebAppContext context)
        {
            //utilizando a consulta context.Users.Where(u => !u.IsAdmin) uma exception era disparada
            Sellers = context.Users.Where(u => u.ID.ToString() != User.ADMIN_ID);
            Cities = context.Cities;
            Classifications = context.Classifications;
            Genders = new List<Gender>() 
            { 
                new Gender() { ID = "M", Name = "M" }, 
                new Gender() { ID = "F", Name = "F" } 
            };
        }

        public IEnumerable<User> Sellers { get; private set; }

        public IEnumerable<City> Cities { get; private set; }

        public IEnumerable<Classification> Classifications { get; private set; }

        public IEnumerable<Gender> Genders { get; private set; }
    }
}