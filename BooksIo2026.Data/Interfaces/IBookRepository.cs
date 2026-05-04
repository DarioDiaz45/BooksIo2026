using BooksIo2026.Entities;

namespace BooksIo2026.Data.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book? GetById(int id);
        void Add(Book book);
        void Delete(int id);
        bool Exist(string title, int bookId);
    }
}
