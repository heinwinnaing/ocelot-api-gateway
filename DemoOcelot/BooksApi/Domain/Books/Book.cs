using BooksApi.Domain.Authors;
using BooksApi.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Domain.Books;

public class Book
    : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid AuthorId { get; set; }
    [ForeignKey(nameof(AuthorId))]
    public virtual Author Author { get; set; } = null!;
}
