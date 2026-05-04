using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Book;

namespace BooksIo2026.Service.Interfaces
{
    public interface IBookService
    {
        List<BookListDto> GetAll();
        BookDetailsDto GetById(int id);
        BookUpdateDto? GetForUpdate(int id);
        Result Add(BookCreateDto dto);
        Result Update(BookUpdateDto dto, bool isActive);
        Result Delete(int id);
    }
}
