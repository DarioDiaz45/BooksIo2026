using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksIo2026.Data.Configurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Price).HasColumnType("decimal(18,2)");
            builder.Property(b => b.AuthorId).ValueGeneratedOnAdd();
            builder.Property(b => b.PublisherId).ValueGeneratedOnAdd();





        }


    }
}
