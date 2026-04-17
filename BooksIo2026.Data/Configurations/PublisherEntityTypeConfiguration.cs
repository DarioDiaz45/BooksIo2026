

using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksIo2026.Data.Configurations
{
    public class PublisherEntityTypeConfiguration :IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Country).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).HasMaxLength(100);
        }
    }
}
