using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Author;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Mappers;
using FluentValidation;

namespace BooksIo2026.Service.Services
{
    public class AuthorService : IAuthorService
    {
        
        private readonly IValidator<Author> _validator;
        private readonly IUnitOfWork _uow;
        public AuthorService(IUnitOfWork unitOfWork, IValidator<Author> validator)
        {
            _validator = validator;
            _uow = unitOfWork;
        }



        public (bool Success, List<string> Errors) Add(AuthorCreateDto authorDto)
        {
            var author = AuthorMapper.toEntity(authorDto);
            var result = _validator.Validate(author);
            if (!result.IsValid)
            {
                var results = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, results);
            }
            if (!_uow.Authors.Exist(author.FirstName, author.LastName))
            {
                try
                {
                    _uow.Authors.Add(author);
                    _uow.Save();
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
                _uow.Authors.Delete(id);
                _uow.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {

                return (false, new List<string> { "An error occurred while deleting the author." });
            }
        }

        public List<AuthorListDto> GetAll()
        {
            return _uow.Authors.GetAll().Select(a => AuthorMapper.ToAuthorListDto(a)).ToList();
        }

        public AuthorDetailsDto GetById(int id)
        {
            var author = _uow.Authors.GetById(id);
            if (author == null) return null!;
            return AuthorMapper.ToAuthorDetailsDto(author);
        }

        public AuthorUpdateDto? GetForUpdate(int id)
        {
            var author = _uow.Authors.GetById(id);
            if (author == null) return null;
            return AuthorMapper.ToAuthorUpdateDto(author);

        }

        public (bool Success, List<string> Errors) Update(AuthorUpdateDto authorDto)
        {
            //var author = AuthorMapper.toEntity(authorDto);
            var author = _uow.Authors.GetById(authorDto.AuthorId);
            if (author == null)
            {
                return (false, new List<string> { "Author not found." });
            }

            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;


            var result = _validator.Validate(author);
            if (!result.IsValid)
            {
                var results = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, results);
            }
            if (!_uow.Authors.Exist(author.FirstName, author.LastName, author.AuthorId))
            {
                try
                {
                    //_repository.Update(author);
                    _uow.Save();
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
