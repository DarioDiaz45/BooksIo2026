using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksIo2026.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public void Add(Author author)
        {
            using (var context = new BooksDbContext())
            {
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new BooksDbContext())
            {
                var author = context.Authors.Find(id);
                if (author == null)
                    return;

                context.Authors.Remove(author);
                context.SaveChanges();

            }
        }

        public bool Exist(string firstName, string lastName, int? authorId = null)
        {
            using (var context = new BooksDbContext())
            {
                Author? author;
                if (authorId == null)
                {
                    author = context.Authors.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);

                }
                else
                {
                    author = context.Authors.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName && a.AuthorId != authorId);
                }
                return author != null;
            }
        }

        public List<Author> GetAll()
        {
            using (var context = new BooksDbContext())
            {
                var authors = context.Authors.AsNoTracking().ToList();
                return authors;
            }
        }


        public Author GetById(int id)
        {
            using (var context = new BooksDbContext())
            {
                return context.Authors.Find(id)!;

            }
        }

        public void Update(Author author)
        {
            using (var context = new BooksDbContext())
            {
                context.Authors.Update(author);
                context.SaveChanges();
            }
        }
    }
}
