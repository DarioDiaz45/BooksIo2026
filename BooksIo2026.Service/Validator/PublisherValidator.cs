using BooksIo2026.Entities;
using FluentValidation;

namespace BooksIo2026.Service.Validator
{
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is Required")
                .MaximumLength(100).MinimumLength(3)
                .WithMessage("Name must be between 3 and 100 characters");

            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("Country is Required")
                .MaximumLength(50).MinimumLength(3)
                .WithMessage("Country must be between 3 and 50 characters");

            RuleFor(p => p.Email)
                .EmailAddress().When(p => !string.IsNullOrEmpty(p.Email))
                .WithMessage("Invalid Email format");
        }
    }
}
