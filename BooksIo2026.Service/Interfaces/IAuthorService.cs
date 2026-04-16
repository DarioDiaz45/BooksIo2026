using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Author;

namespace BooksIo2026.Service.Interfaces
{
    public interface IAuthorService
    {
        List<AuthorListDto> GetAll();
        AuthorDetailsDto GetById(int id);
        AuthorUpdateDto? GetForUpdate(int id);
        (bool Success, List<string> Errors) Add(AuthorCreateDto authorDto);
        (bool Success, List<string> Errors) Update(AuthorUpdateDto authorDto);
        (bool Success, List<string> Errors) Delete(int id);
    }
}
