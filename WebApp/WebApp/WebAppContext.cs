using System.Collections.Generic;
using System.Data.Entity;
using WebApp.Models;

namespace WebApp
{
    public class WebAppContext: DbContext
    {
        public WebAppContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(p => p.ID);
            modelBuilder.Entity<User>().Property(p => p.ID).HasColumnName("UserId");
            modelBuilder.Entity<User>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Password).IsRequired();

            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<City>().HasKey(p => p.ID);
            modelBuilder.Entity<City>().Property(p => p.ID).HasColumnName("CityId");
            modelBuilder.Entity<City>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<City>().Property(p => p.Name).HasColumnName("CityName");

            modelBuilder.Entity<Classification>().ToTable("Classification");
            modelBuilder.Entity<Classification>().HasKey(p => p.ID);
            modelBuilder.Entity<Classification>().Property(p => p.ID).HasColumnName("ClassificationId");
            modelBuilder.Entity<Classification>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Classification>().Property(p => p.Name).HasColumnName("ClassificationName");

            modelBuilder.Entity<Region>().ToTable("Region");
            modelBuilder.Entity<Region>().HasKey(p => p.ID);
            modelBuilder.Entity<Region>().Property(p => p.ID).HasColumnName("RegionId");
            modelBuilder.Entity<Region>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Region>().Property(p => p.Name).HasColumnName("RegionName");
            modelBuilder.Entity<Region>().Property(p => p.CityID).IsRequired();
            modelBuilder.Entity<Region>().Property(p => p.CityID).HasColumnName("CityId");
            

            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Client>().HasKey(p => p.ID);
            modelBuilder.Entity<Client>().Property(p => p.ID).HasColumnName("ClientId");
            modelBuilder.Entity<Client>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Client>().Property(p => p.Name).HasColumnName("Name");
            modelBuilder.Entity<Client>().Property(p => p.Phone).IsRequired();
            modelBuilder.Entity<Client>().Property(p => p.Phone).HasColumnName("Phone");
            modelBuilder.Entity<Client>().Property(p => p.SellerID).IsRequired();
            modelBuilder.Entity<Client>().Property(p => p.SellerID).HasColumnName("SellerId");
            modelBuilder.Entity<Client>().Property(p => p.RegionID).IsRequired();
            modelBuilder.Entity<Client>().Property(p => p.RegionID).HasColumnName("RegionId");
            modelBuilder.Entity<Client>().Property(p => p.ClassificationID).IsRequired();
            modelBuilder.Entity<Client>().Property(p => p.ClassificationID).HasColumnName("ClassificationId");
            modelBuilder.Entity<Client>().Property(p => p.Gender).HasColumnName("Gender");
            modelBuilder.Entity<Client>().Property(p => p.Gender).IsOptional();
            modelBuilder.Entity<Client>().Property(p => p.LastPurchase).HasColumnName("LastPurchase");
            modelBuilder.Entity<Client>().Property(p => p.LastPurchase).IsOptional();
            modelBuilder.Entity<Client>().Property(p => p.Occupation).HasColumnName("Occupation");
            modelBuilder.Entity<Client>().Property(p => p.Occupation).IsOptional();
            modelBuilder.Entity<Client>().Property(p => p.Address).HasColumnName("Address");
            modelBuilder.Entity<Client>().Property(p => p.Address).IsOptional();

            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<Client> GetCustomers()
        {
            foreach (var client in Clients)
            {
                Entry(client).Reference(r => r.Seller).Load();
                Entry(client).Reference(r => r.Region).Load();
                Entry(client).Reference(r => r.Classification).Load();
            }

            return Clients;
        }
    }
}