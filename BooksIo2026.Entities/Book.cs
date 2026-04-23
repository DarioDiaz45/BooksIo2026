namespace BooksIo2026.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } = null!;

        public DateTime PublishedDate { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}
