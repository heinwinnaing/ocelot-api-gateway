using FluentValidation;

namespace BooksApi.Queries.GetBookById;

public class GetBookByIdQueryValidator
    : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {

    }
}