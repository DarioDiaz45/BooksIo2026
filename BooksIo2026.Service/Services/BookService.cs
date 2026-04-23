using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Book;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Mappers;
using FluentValidation;

namespace BooksIo2026.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Book> _validator;
        public BookService(IBookRepository repository, IUnitOfWork unitOfWork , IValidator<Book> validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public (bool Success, List<string> Errors) Add(BookCreateDto dto, bool isActive)
        {
            var book = BookMapper.toEntity(dto);

            book.IsActive = isActive;

            var result = _validator.Validate(book);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            try
            {
                _repository.Add(book);
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {
                return (false, new List<string> { "An error occurred while adding the book." });
            }
        }

        public (bool Success, List<string> Errors) Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {
                return (false, new List<string> { "An error occurred while deleting the book." });
            }
        }

        public List<BookListDto> GetAll()
        {
            return _repository.GetAll()
                .Select(b => BookMapper.ToBookListDto(b))
                .ToList();
        }

        public BookDetailsDto GetById(int id)
        {
            var book = _repository.GetById(id);
            if (book == null) return null!;

            return BookMapper.ToBookDetailsDto(book);
        }

        public BookUpdateDto? GetForUpdate(int id)
        {
            var book = _repository.GetById(id);
            if (book == null) return null;

            return BookMapper.ToBookUpdateDto(book);
        }

        public (bool Success, List<string> Errors) Update(BookUpdateDto dto, bool isActive)
        {
            var book = _repository.GetById(dto.BookId);

            if (book == null)
            {
                return (false, new List<string> { "Book not found." });
            }

            book.Title = dto.Title;
            book.AuthorId = dto.AuthorId;
            book.PublisherId = dto.PublisherId;
            book.PublishedDate = dto.PublishedDate;
            book.Price = dto.Price;
            book.IsActive = isActive;

            var result = _validator.Validate(book);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            try
            {
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {
                return (false, new List<string> { "An error occurred while updating the book." });
            }


        }
    }
}
