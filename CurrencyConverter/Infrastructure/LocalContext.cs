using CurrencyConverter.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Infrastructure
{
    public class LocalContext : DbContext
    {
        public LocalContext(DbContextOptions<LocalContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}