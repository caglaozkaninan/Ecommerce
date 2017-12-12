using ECommerce.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dal
{
  public class ECommerceDbContext :DbContext
    {
        public ECommerceDbContext() : base("ECommerceDbConnection")
        {
            Database.SetInitializer<ECommerceDbContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Basket_item> Basket_items { get; set; }
        public DbSet<Shippingmethod> Shippingmethods { get; set; }
        public DbSet<Paymentmethod> Paymentmethods { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }


    }
}
