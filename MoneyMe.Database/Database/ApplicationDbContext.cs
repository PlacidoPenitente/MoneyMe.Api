using Microsoft.EntityFrameworkCore;
using MoneyMe.Infrastructure.Database.Models;

namespace MoneyMe.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<ProductFee> ProductFees { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fee>().HasIndex(fee => fee.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(product => product.Name).IsUnique();
            modelBuilder.Entity<Rule>().HasIndex(rule => rule.Name).IsUnique();

            modelBuilder.Entity<ProductFee>().HasOne(x => x.Fee).WithMany(x => x.ProductFees).HasForeignKey(x => x.FeeId);
            modelBuilder.Entity<ProductFee>().HasOne(x => x.Product).WithMany(x => x.ProductFees).HasForeignKey(x => x.ProductId);
        }
    }
}