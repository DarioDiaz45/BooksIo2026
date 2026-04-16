using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksIo2026.Data.Configurations
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>  
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasIndex(a => new { a.FirstName, a.LastName })
                .IsUnique().HasDatabaseName("IX_Author_FirstName_LastName");
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
        }
    }
}
