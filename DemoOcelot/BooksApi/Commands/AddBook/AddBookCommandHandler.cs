using BooksApi.Domain;
using BooksApi.Domain.Books;
using BooksApi.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Commands.AddBook;

public class AddBookCommandHandler(
    AddBookCommandValidator validator,
    IDbContext dbContext)
    : IRequestHandler<AddBookCommand, ResultModel<AddBookCommandDto>>
{
    public async Task<ResultModel<AddBookCommandDto>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
        {
            var errors = validationResult
                .Errors
                .Select(s => s.ErrorMessage)
                .ToArray();
            return ResultModel<AddBookCommandDto>.Error(400, errors);
        }
        
        var author = await dbContext
            .Authors
            .FirstOrDefaultAsync(r => r.Id == request.AuthorId, cancellationToken);
        if (author is null)
            return ResultModel<AddBookCommandDto>.Error(400, $"Invalid author");

        var book = new Book
        {
            Id = Guid.NewGuid(),
            AuthorId = author.Id,
            Title = request.Title,
            Description = request.Description,
        };
        await dbContext
            .Books
            .AddAsync(book, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return ResultModel<AddBookCommandDto>.Success(new AddBookCommandDto { Id = book.Id });
    }
}