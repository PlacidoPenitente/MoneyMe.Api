using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.QuoteAggregate;

namespace MoneyMe.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Quote> Quotes { get; set; }
    }
}