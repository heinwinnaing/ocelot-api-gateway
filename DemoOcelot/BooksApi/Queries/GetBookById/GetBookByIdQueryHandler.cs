using BooksApi.Domain;
using BooksApi.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Queries.GetBookById;

public class GetBookByIdQueryHandler(
    IDbContext dbContext)
    : IRequestHandler<GetBookByIdQuery, ResultModel<GetBookByIdQueryDto>>
{
    public async Task<ResultModel<GetBookByIdQueryDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await dbContext
            .Books
            .Include(i => i.Author)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (book is null)
            return ResultModel<GetBookByIdQueryDto>.Error(400, $"Book with Id:{request.Id} was not found.");

        return ResultModel<GetBookByIdQueryDto>.Success(new GetBookByIdQueryDto 
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Author = new AuthorDto
            {
                Id = book.AuthorId,
                Name = book.Author.Name,
            }
        });
    }
}