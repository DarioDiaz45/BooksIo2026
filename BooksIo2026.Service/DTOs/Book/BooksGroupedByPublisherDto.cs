namespace BooksIo2026.Service.DTOs.Book
{
    public class BooksGroupedByPublisherDto
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = null!;
        public int TotalCount { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
