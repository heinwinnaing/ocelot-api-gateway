using BooksApi.Model;

namespace BooksApi.Queries.GetBookList;

public class GetBookListQueryDto
    : BookDto
{
    public AuthorDto? Author { get; set; }
}
