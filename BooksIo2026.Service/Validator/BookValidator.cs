using BooksIo2026.Entities;
using FluentValidation;

namespace BooksIo2026.Service.Validator
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is Required")
                .MaximumLength(100).MinimumLength(3)
                .WithMessage("Title must be between 3 and 100 characters");

            RuleFor(b => b.AuthorId)
                .GreaterThan(0).WithMessage("Author is required");

            RuleFor(b => b.PublisherId)
                .GreaterThan(0).WithMessage("Publisher is required");

            RuleFor(b => b.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
