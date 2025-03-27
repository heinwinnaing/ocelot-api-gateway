using BooksApi.Domain;
using BooksApi.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Queries.GetBookList;

public class GetBookListQueryHandler(
    IDbContext dbContext)
    : IRequestHandler<GetBookListQuery, ResultModel<List<GetBookListQueryDto>>>
{
    public async Task<ResultModel<List<GetBookListQueryDto>>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
    {
        var books = await dbContext
            .Books
            .Include(i => i.Author)
            .ToListAsync(cancellationToken);

        var data = books
            .Select(s => new GetBookListQueryDto 
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Author = new AuthorDto { Id = s.AuthorId, Name = s.Author.Name}
            })
            .ToList();

        return ResultModel<List<GetBookListQueryDto>>.Success(data);
    }
}
