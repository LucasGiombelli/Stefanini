using System;

namespace WebApp.Models
{
    public class Region : AbstractModel
    {
        public Guid CityID { get; set; }

        public virtual City City { get; set; }
    }
}