using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Book;

namespace BooksIo2026.Service.Mappers
{
    public static class BookMapper
    {
        public static BookListDto ToBookListDto(Book book)
        {
            return new BookListDto
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                PublisherName = book.Publisher.Name
            };
        }

        public static BookUpdateDto ToBookUpdateDto(Book book)
        {
            return new BookUpdateDto
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                PublishedDate = book.PublishedDate,
                Price = book.Price
            };
        }

        public static Book toEntity(BookUpdateDto dto)
        {
            return new Book
            {
                BookId = dto.BookId,
                Title = dto.Title,
                AuthorId = dto.AuthorId,
                PublisherId = dto.PublisherId,
                PublishedDate = dto.PublishedDate,
                Price = dto.Price
            };
        }

        public static BookDetailsDto ToBookDetailsDto(Book book)
        {
            return new BookDetailsDto
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                PublisherName = book.Publisher.Name,
                PublishedDate = book.PublishedDate,
                Price = book.Price
            };
        }

        public static Book toEntity(BookCreateDto dto)
        {
            return new Book
            {
                Title = dto.Title,
                AuthorId = dto.AuthorId,
                PublisherId = dto.PublisherId,
                PublishedDate = dto.PublishedDate,
                Price = dto.Price
            };
        }
    }
}

