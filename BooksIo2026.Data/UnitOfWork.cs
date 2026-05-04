using BooksIo2026.Data.Interfaces;

namespace BooksIo2026.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BooksDbContext _context;
        public UnitOfWork(BooksDbContext context, IAuthorRepository authors, IBookRepository books, IPublisherRepository publishers)
        {
            _context = context;
            Authors = authors;
            Books = books;
            Publishers = publishers;
        }

        public IAuthorRepository Authors { get; private set; }

        public IBookRepository Books { get; private set; }

        public IPublisherRepository Publishers { get; private set; }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
