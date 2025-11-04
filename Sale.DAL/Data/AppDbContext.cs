using Microsoft.EntityFrameworkCore;
using Sale.Models.Entities;

namespace Sale.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Seller> Sellers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Seller>()
                .HasMany(s => s.Products)
                .WithOne(p => p.Seller)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.Product)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
               .Property(p => p.Price)
               .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.OriginalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
       .Property(c => c.UnitPrice)
       .HasPrecision(18, 2);
        }
    }
}
