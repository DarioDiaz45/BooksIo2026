using BooksIo2026.Service.DTOs.Book;

namespace BooksIo2026.Service.Interfaces
{
    public interface IBookService
    {
        List<BookListDto> GetAll();
        BookDetailsDto GetById(int id);
        BookUpdateDto? GetForUpdate(int id);
        (bool Success, List<string> Errors) Add(BookCreateDto dto);
        (bool Success, List<string> Errors) Update(BookUpdateDto dto, bool isActive);
        (bool Success, List<string> Errors) Delete(int id);
    }
}
