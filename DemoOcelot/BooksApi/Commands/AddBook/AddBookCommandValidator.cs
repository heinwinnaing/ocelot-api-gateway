using FluentValidation;

namespace BooksApi.Commands.AddBook;

public class AddBookCommandValidator
    : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(r => r.Description)
            .MaximumLength(1000);

        RuleFor(r => r.AuthorId)
            .NotEqual(Guid.Empty);
    }
}