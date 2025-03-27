using BooksApi.Domain.Authors;
using BooksApi.Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Domain;

public interface IDbContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Author> Authors { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
