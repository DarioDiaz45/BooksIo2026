using BooksIo2026.Data.Configurations;
using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BooksIo2026.Data
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=BooksIo2026; Trusted_Connection=true; TrustServerCertificate=true;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksDbContext).Assembly);
        }
    }
}
