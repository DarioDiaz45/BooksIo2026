using BooksIo2026.Data.Interfaces;

namespace BooksIo2026.Data
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IPublisherRepository Publishers { get; }
        void Save();

    }
}
