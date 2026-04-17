using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksIo2026.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BooksDbContext _context;
        public AuthorRepository(BooksDbContext context)
        {
            _context = context;
        }
        public void Add(Author author)
        {

            _context.Authors.Add(author);

        }

        public void Delete(int id)
        {

            var author = _context.Authors.Find(id);
            if (author == null)
                return;
            _context.Authors.Remove(author);
        }

        public bool Exist(string firstName, string lastName, int? authorId = null)
        {

            Author? author;
            if (authorId == null)
            {
                author = _context.Authors.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);

            }
            else
            {
                author = _context.Authors.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName && a.AuthorId != authorId);
            }
            return author != null;
        }

        public List<Author> GetAll()
        {
            return _context.Authors.AsNoTracking().ToList();
        }


        public Author GetById(int id)
        {
            return _context.Authors.Find(id)!;
        }

        public void Update(Author author)
        {

            _context.Authors.Update(author);
        }
    }
}
