using BooksIo2026.Data;
using BooksIo2026.Entities;
using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Book;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Mappers;
using FluentValidation;

namespace BooksIo2026.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<Book> _validator;
        public BookService(IUnitOfWork unitOfWork, IValidator<Book> validator)
        {

            _uow = unitOfWork;
            _validator = validator;
        }
        public Result Add(BookCreateDto bookDto)
        {
            var book = BookMapper.toEntity(bookDto);

            var result = _validator.Validate(book);
            if (!result.IsValid)
            {

                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_uow.Books.ExistSameName(book.Title, book.BookId))
            {
                return Result.Failure("Book already exist!!!");

            }
            try
            {
                _uow.Books.Add(book);
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
            var book = _uow.Books.GetById(id);
            if (book == null)
            {
                return Result.Failure("Book not found.");
            }
            try
            {
                _uow.Books.Delete(id);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public List<BookListDto> GetAll()
        {
            return _uow.Books.GetAll()
                .Select(b => BookMapper.ToBookListDto(b))
                .ToList();
        }

        public BookDetailsDto GetById(int id)
        {
            var book = _uow.Books.GetById(id);
            if (book == null) return null!;

            return BookMapper.ToBookDetailsDto(book);
        }

        public BookUpdateDto? GetForUpdate(int id)
        {
            var book = _uow.Books.GetById(id);
            if (book == null) return null;

            return BookMapper.ToBookUpdateDto(book);
        }

        public Result Update(BookUpdateDto bookDto, bool isActive)
        {
            var bookToValidate = BookMapper.toEntity(bookDto);
            var result = _validator.Validate(bookToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            Book? book = _uow.Books.GetById(bookDto.BookId);
            if (book == null)
            {
                return Result.Failure("Book Not Found");

            }

            book.Title = bookDto.Title;
            book.AuthorId = bookDto.AuthorId;
            book.PublisherId = bookDto.PublisherId;
            book.PublishedDate = bookDto.PublishedDate;
            book.Price = bookDto.Price;
            book.IsActive = bookDto.IsActive;

            if (_uow.Books.ExistSameName(book.Title, book.BookId))
            {
                return Result.Failure("Book already exist!!!");

            }
            try
            {
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
