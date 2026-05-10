using BooksIo2026.Entities;
using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Author;

namespace BooksIo2026.Service.Interfaces
{
    public interface IAuthorService
    {
        Result<List<AuthorListDto>> GetAll();
        Result<AuthorListDto> GetById(int id);
        Result<AuthorUpdateDto> GetForUpdate(int id);
        Result Add(AuthorCreateDto authorDto);
        Result Update(AuthorUpdateDto authorDto);
        Result Delete(int id);
    }
}
