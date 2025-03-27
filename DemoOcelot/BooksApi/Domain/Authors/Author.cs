using BooksApi.Domain.Base;

namespace BooksApi.Domain.Authors;

public class Author
    : BaseEntity
{
    public string Name { get; set; } = null!;
}
