using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksIo2026.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordsToAuthorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Authors (FirstName, LastName) values ('Jane', 'Austen')");
            migrationBuilder.Sql("Insert into Authors (FirstName, LastName) values ('Isaac', 'Asimov')");
            migrationBuilder.Sql("Insert into Authors (FirstName, LastName) values ('Arthur C.', 'Clarke')");
            migrationBuilder.Sql("Insert into Authors (FirstName, LastName) values ('Ursula', 'Le Guin')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete * from Authors");
        }
    }
}
