using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Author;

namespace BooksIo2026.Service.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorListDto ToAuthorListDto(Author author)
        {
            return new AuthorListDto
            {
                AuthorId = author.AuthorId,
                FullName = $"{author.FirstName} {author.LastName}"
            };

        }
        public static AuthorUpdateDto ToAuthorUpdateDto(Author author)
        {
            return new AuthorUpdateDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

        }
       
        public static Author toEntity(AuthorUpdateDto authorDto)
        {
            return new Author
            {
                AuthorId = authorDto.AuthorId,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };
        }
        public static AuthorDetailsDto ToAuthorDetailsDto(Author author)
        {
            return new AuthorDetailsDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }
        public static Author toEntity(AuthorCreateDto authorDto)
        {
            return new Author
            {
               
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };
        }
    }
}
