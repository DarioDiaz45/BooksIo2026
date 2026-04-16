using BooksIo2026.Data.Interfaces;
using BooksIo2026.Data.Repositories;
using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Author;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Validator;

namespace BooksIo2026.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly AuthorValidator _validator;
        public AuthorService()
        {
            _repository = new AuthorRepository();
            _validator = new AuthorValidator();
        }



        public (bool Success, List<string> Errors) Add(AuthorCreateDto authorDto)
        {
            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };
            var result = _validator.Validate(author);
            if (!result.IsValid)
            {
                var results = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, results);
            }
            if (!_repository.Exist(author.FirstName, author.LastName))
            {
                try
                {
                    _repository.Add(author);
                    return (true, new List<string>());
                }
                catch (Exception)
                {

                    return (false, new List<string> { "An error occurred while adding the author." });
                }
            }
            else
            {
                return (false, new List<string> { "An author with the same name already exists." });
            }
        }

        public (bool Success, List<string> Errors) Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return (true, new List<string>());
            }
            catch (Exception)
            {

                return (false, new List<string> { "An error occurred while deleting the author." });
            }
        }

        public List<AuthorListDto> GetAll()
        {
            return _repository.GetAll().Select(a => new AuthorListDto
            {
                AuthorId = a.AuthorId,
                FullName = $"{a.FirstName} {a.LastName}"
            }).ToList();
        }

        public AuthorDetailsDto GetById(int id)
        {
            var author = _repository.GetById(id);
            if(author == null) return null!;
            return new AuthorDetailsDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public AuthorUpdateDto? GetForUpdate(int id)
        {
            var author = _repository.GetById(id);
            if (author == null) return null;
            return new AuthorUpdateDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

        }

        public (bool Success, List<string> Errors) Update(AuthorUpdateDto authorDto)
        {
            var author = new Author
            {
                AuthorId = authorDto.AuthorId,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };
            var result = _validator.Validate(author);
            if (!result.IsValid)
            {
                var results = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, results);
            }
            if (!_repository.Exist(author.FirstName,author.LastName,author.AuthorId))
            {
                try
                {
                    _repository.Update(author);
                    return (true, new List<string>());
                }
                catch (Exception)
                {

                    return (false, new List<string> { "An error occurred while updating the author." });
                } 
            }
            else
            {
                return (false, new List<string> { "Author already exist!!!" });
            }
        }
    }
}
