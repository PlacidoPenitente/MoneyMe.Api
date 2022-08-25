using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.ApplicationAggregate;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.QuoteAggregate;
using System;

namespace MoneyMe.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().OwnsMany(x => x.Terms);

            //modelBuilder.Entity<Product>().HasData(
            //    new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product A", 0, 3),
            //    new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product B", 0.0949m, 6),
            //    new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product C", 0.0949m, 12),
            //    new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product D", 0.0949m, 24)
            //    );
        }
    }
}