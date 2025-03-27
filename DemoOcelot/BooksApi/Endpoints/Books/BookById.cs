
using BooksApi.Model;
using BooksApi.Queries.GetBookById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Endpoints.Books;

public class BookById
    : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/books/{id:guid}", async (
            [FromRoute]Guid id,
            CancellationToken cancellationToken,
            IMediator mediator) => 
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await mediator.Send(new GetBookByIdQuery { Id = id }, cancellationToken);
            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result);
        })
            .WithTags("Books")
            .WithSummary("[Get book details]")
            .HasApiVersion(1)
            .Produces<ResultModel<GetBookByIdQueryDto>>(200, "application/json");
    }
}
