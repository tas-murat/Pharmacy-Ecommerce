using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.DAL.Contexts
{
    public class SqlContext:DbContext
    {
        public SqlContext():base("EczaneCS1")
        {}

        public DbSet<Product> Product { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<ProductProperty> ProductProperty { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<FAQCategory> FAQCategory { get; set; }
        public DbSet<Contacte> Contacte { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<FooterE> FooterE { get; set; }
        public DbSet<HomeE> HomeE { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Advertisement> Advertisement { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>().HasIndex(hi => hi.OrderNumber).IsUnique();
        }
    }
}
