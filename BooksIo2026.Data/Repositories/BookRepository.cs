using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksIo2026.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksDbContext _context;
        public BookRepository(BooksDbContext context)
        {
            _context = context;
        }
        public List<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.BookId == id);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }

        public bool ExistSameName(string title, int bookId)
        {
            return _context.Books.Any(b => b.Title == title && b.BookId != bookId);
        }
    }
}
