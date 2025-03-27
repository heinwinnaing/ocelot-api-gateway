
using BooksApi.Model;
using BooksApi.Queries.GetBookList;
using MediatR;

namespace BooksApi.Endpoints.Books;

public class BookList
    : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("books", async (
            [AsParameters]GetBookListQuery query,
            CancellationToken cancellationToken,
            IMediator mediator) =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await mediator.Send(query, cancellationToken);

            return Results.Ok(result);
        })
            .WithTags("Books")
            .WithSummary("[Get book list]")
            .HasApiVersion(1)
            .Produces<ResultModel<List<GetBookListQueryDto>>>(200, "application/json");
    }
}
