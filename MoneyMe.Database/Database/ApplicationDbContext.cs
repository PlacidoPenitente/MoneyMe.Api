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
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}