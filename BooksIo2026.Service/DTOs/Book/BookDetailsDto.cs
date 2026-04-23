namespace BooksIo2026.Service.DTOs.Book
{
    public class BookDetailsDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string PublisherName { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }

    }
}
