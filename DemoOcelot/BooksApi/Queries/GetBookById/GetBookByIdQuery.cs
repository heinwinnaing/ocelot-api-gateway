using BooksApi.Model;
using MediatR;

namespace BooksApi.Queries.GetBookById;

public class GetBookByIdQuery
    : IRequest<ResultModel<GetBookByIdQueryDto>>
{
    public Guid Id { get; set; }
}
