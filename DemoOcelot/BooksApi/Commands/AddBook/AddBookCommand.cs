using BooksApi.Model;
using MediatR;

namespace BooksApi.Commands.AddBook;

public class AddBookCommand
    : IRequest<ResultModel<AddBookCommandDto>>
{
    public Guid AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}
