namespace BooksIo2026.Service.DTOs.Author
{
    public class AuthorUpdateDto
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
