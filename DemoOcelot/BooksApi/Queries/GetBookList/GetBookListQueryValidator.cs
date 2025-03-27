using FluentValidation;

namespace BooksApi.Queries.GetBookList;

public class GetBookListQueryValidator
    : AbstractValidator<GetBookListQuery>
{
    public GetBookListQueryValidator()
    {

    }
}