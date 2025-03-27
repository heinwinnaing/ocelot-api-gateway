using BooksApi.Model;
using MediatR;

namespace BooksApi.Queries.GetBookList;

public class GetBookListQuery
    : IRequest<ResultModel<List<GetBookListQueryDto>>>
{

}
