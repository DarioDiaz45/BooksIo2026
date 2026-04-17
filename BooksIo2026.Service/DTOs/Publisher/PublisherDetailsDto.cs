using System;
using System.Collections.Generic;
using System.Text;

namespace BooksIo2026.Service.DTOs.Publisher
{
    public class PublisherDetailsDto
    {
        public int PublisherId { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public DateTime FoundedDate { get; set; }
        public string? Email { get; set; }
    }
}
