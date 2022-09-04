using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.ApplicationAggregate;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.Rules;
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
            modelBuilder.Entity<Loan>().OwnsMany(x => x.MonthlyAmortization);
            modelBuilder.Entity<Product>().OwnsMany(x => x.Fees);

            modelBuilder.Entity<Product>().HasData(
                new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product A", 0, 5, 1, nameof(InterestFreeRule)),
                new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product B", 0.0949m, 9, 6, nameof(FirstTwoMonthsInterestFreeRule)),
                new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product C", 0.0949m, 36, 10, nameof(NoInterestFreeRule))
                );
        }
    }
}