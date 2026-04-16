using BooksIo2026.Entities;
using FluentValidation;


namespace BooksIo2026.Service.Validator
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("First Name is Required").
                MaximumLength(50).MinimumLength(3)
                .WithMessage("First Name must be between 3 and 50 characters");
            RuleFor(a => a.LastName).NotEmpty().WithMessage("Last Name is Required")
                .MaximumLength(50).MinimumLength(3).
                WithMessage("Last Name must be between 3 and 50 characters");
        }
    }
}
