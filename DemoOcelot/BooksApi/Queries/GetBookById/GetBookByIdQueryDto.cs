using BooksApi.Model;

namespace BooksApi.Queries.GetBookById;

public class GetBookByIdQueryDto
    : BookDto
{
    public AuthorDto Author { get; set; } = null!;
}