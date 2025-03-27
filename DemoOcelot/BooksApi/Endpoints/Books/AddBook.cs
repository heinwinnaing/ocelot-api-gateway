
using BooksApi.Commands.AddBook;
using BooksApi.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Endpoints.Books;

public class AddBook
    : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("books", async (
            [FromBody]AddBookCommand command,
            CancellationToken cancellationToken,
            IMediator mediator) =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await mediator.Send(command, cancellationToken);
            if(result.IsSuccess) 
                return Results.Ok(result);

            return Results.BadRequest(result);
        })
            .WithTags("Books")
            .WithSummary("[Add new book]")
            .HasApiVersion(1)
            .Accepts<AddBookCommand>("application/json")
            .Produces<ResultModel<AddBookCommandDto>>(200, "application/json");
    }
}
