using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksIo2026.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Books (Title, AuthorId, PublisherId,PublishedDate, Price, IsActive) VALUES ('Pride and Prejudice', 1, 1, '1813-01-28', 2500, 1)");
            migrationBuilder.Sql("INSERT INTO Books (Title, AuthorId, PublisherId,PublishedDate, Price, IsActive) VALUES ('Foundation', 2, 2, '1951-06-01', 3200, 1)");
            migrationBuilder.Sql("INSERT INTO Books (Title, AuthorId, PublisherId,PublishedDate, Price, IsActive) VALUES ('2001: A Space Odyssey', 3, 2, '1968-07-01', 3000, 1)");
            migrationBuilder.Sql("INSERT INTO Books (Title, AuthorId, PublisherId,PublishedDate, Price, IsActive) VALUES ('The Left Hand of Darkness', 4, 3, '1969-03-01', 2800, 1)");
            migrationBuilder.Sql("INSERT INTO Books (Title, AuthorId, PublisherId,PublishedDate, Price, IsActive) VALUES ('Sense and Sensibility', 1, 1, '1811-10-30', 2600, 1)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete * from Books");
        }
    }
}
