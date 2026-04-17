using BooksIo2026.Entities;

namespace BooksIo2026.Data.Interfaces
{
    public interface IPublisherRepository
    {
        List<Publisher> GetAll();
        Publisher? GetById(int id);
        void Add(Publisher publisher);
        void Delete(int id);
    }
}
