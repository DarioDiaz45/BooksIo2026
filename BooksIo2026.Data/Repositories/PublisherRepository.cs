

using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;

namespace BooksIo2026.Data.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BooksDbContext _context;
        public PublisherRepository(BooksDbContext context)
        {
            _context = context;
        }
        public void Add(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
        }

        public void Delete(int id)
        {
            var publisher = _context.Publishers.Find(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
            }
        }

        public bool ExistSameName(string name, int? publisherId = null)
        {
            return _context.Publishers.Any(p =>
              p.Name == name &&
              (publisherId == null || p.PublisherId != publisherId));
        }


        public List<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public Publisher? GetById(int id)
        {
            return _context.Publishers.Find(id);
        }

        public bool HasBooks(int id)
        {
            return _context.Books.Any(b => b.PublisherId == id);
        }

        public void Update(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
        }
    }
}
