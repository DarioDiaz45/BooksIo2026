using BooksIo2026.Entities;

namespace BooksIo2026.Data.Interfaces
{
    public interface IPublisherRepository
    {
        List<Publisher> GetAll();
        Publisher? GetById(int id);
        void Add(Publisher publisher);
        void Delete(int id);
        void Update(Publisher publisher);
        bool ExistSameName(string name, int? publisherId = null);
        bool HasBooks(int id);

    }
}
