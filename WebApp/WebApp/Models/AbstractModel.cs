using System;

namespace WebApp.Models
{
    public abstract class AbstractModel
    {
        public AbstractModel()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}