using BooksApi.Domain;
using BooksApi.Domain.Authors;
using BooksApi.Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Persistence;

public class EFContext
    : DbContext, IDbContext
{
    public EFContext()
    {

    }

    public EFContext(DbContextOptions<EFContext> options)
        : base(options) 
    {

    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity => 
        {
            entity.HasKey(e => e.Id);
        });
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        base.OnModelCreating(modelBuilder);
    }
}
