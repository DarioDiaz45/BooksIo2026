using BooksIo2026.Data;
using BooksIo2026.Entities;
using BooksIo2026.Service.Common;
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



        public Result Add(AuthorCreateDto authorDto)
        {
            var author = AuthorMapper.toEntity(authorDto);
            var result = _validator.Validate(author);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_uow.Authors.ExistSameName(author.FirstName, author.LastName))
            {
                return Result.Failure("An author with the same name already exists.");
            }
            try
            {
                _uow.Authors.Add(author);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }


        }

        public Result Delete(int id)
        {
            var author = _uow.Authors.GetById(id);
            if (author == null)
            {
                return Result.Failure("Author not found.");
            }
            if (_uow.Authors.HasBooks(id))
            {
                return Result.Failure("Cannot delete an author with associated books.");
            }
            try
            {
                _uow.Authors.Delete(id);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<AuthorListDto>> GetAll()
        {
            var authors= _uow.Authors.GetAll().Select(a => AuthorMapper.ToAuthorListDto(a)).ToList();
            return Result<List<AuthorListDto>>.Success(authors);
        }

        public Result<AuthorListDto> GetById(int id)
        {
            var author = _uow.Authors.GetById(id);
            if (author == null) return Result<AuthorListDto>.Failure("Author not found.");
            return Result<AuthorListDto>.Success(AuthorMapper.ToAuthorListDto(author));

        }

        public Result<AuthorUpdateDto> GetForUpdate(int id)
        {
            var author = _uow.Authors.GetById(id);
            if (author == null) return Result<AuthorUpdateDto>.Failure("Author not found.");
            return Result<AuthorUpdateDto>.Success(AuthorMapper.ToAuthorUpdateDto(author));
        }

        public Result Update(AuthorUpdateDto authorDto)
        {
            var authorToValidate = AuthorMapper.toEntity(authorDto);
            var result = _validator.Validate(authorToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var author = _uow.Authors.GetById(authorDto.AuthorId);
            if (author == null)
            {
                return Result.Failure("Author not found.");
            }

            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;



            if (_uow.Authors.ExistSameName(author.FirstName, author.LastName, author.AuthorId))
            {
                return Result.Failure("Author already exist!!!");
            }

            try
            {
                //_repository.Update(author);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

    }
}
