using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksIo2026.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecreatePublisherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "Publishers",
             columns: table => new
             {
              PublisherId = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
              Name = table.Column<string>(nullable: false),
              Country = table.Column<string>(nullable: false),
              FoundedDate = table.Column<DateTime>(nullable: false),
              Email = table.Column<string>(nullable: true),
              IsActive = table.Column<bool>(nullable: false)
              },
              constraints: table =>
              {
                table.PrimaryKey("PK_Publishers", x => x.PublisherId);
              });
            migrationBuilder.Sql("INSERT INTO Publishers (Name, Country, FoundedDate, Email, IsActive) VALUES ('Planeta', 'Argentina', '1949-01-01', 'planeta@mail.com', 1)");
            migrationBuilder.Sql("INSERT INTO Publishers (Name, Country, FoundedDate, Email, IsActive) VALUES ('Penguin', 'USA', '1935-01-01', 'penguin@mail.com', 1)");
            migrationBuilder.Sql("INSERT INTO Publishers (Name, Country, FoundedDate, Email, IsActive) VALUES ('Santillana', 'España', '1960-01-01', 'santillana@mail.com', 1)");
            migrationBuilder.Sql("INSERT INTO Publishers (Name, Country, FoundedDate, Email, IsActive) VALUES ('Anagrama', 'España', '1969-01-01', 'anagrama@mail.com', 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete * from Publishers");
        }
    }
}
