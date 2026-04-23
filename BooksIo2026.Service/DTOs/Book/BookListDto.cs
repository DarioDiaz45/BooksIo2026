namespace BooksIo2026.Service.DTOs.Book
{
    public class BookListDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string PublisherName { get; set; } = null!;

    }
}
