using Core.DataAccess.Configs;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.Contexts
{
    public class ECommerceContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);
        }
    }
}
