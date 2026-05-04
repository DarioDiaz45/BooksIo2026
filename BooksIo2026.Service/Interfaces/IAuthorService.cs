using BooksIo2026.Entities;
using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Author;

namespace BooksIo2026.Service.Interfaces
{
    public interface IAuthorService
    {
        List<AuthorListDto> GetAll();
        AuthorDetailsDto GetById(int id);
        AuthorUpdateDto? GetForUpdate(int id);
        Result Add(AuthorCreateDto authorDto);
        Result Update(AuthorUpdateDto authorDto);
        Result Delete(int id);
    }
}
