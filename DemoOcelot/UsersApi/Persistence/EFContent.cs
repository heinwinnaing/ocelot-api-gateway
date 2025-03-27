using Microsoft.EntityFrameworkCore;
using UsersApi.Domain;
using UsersApi.Domain.Users;

namespace UsersApi.Persistence;

public class EFContent
    : DbContext, IDbContext
{
    public EFContent()
    { }
    public EFContent(DbContextOptions<EFContent> options)
        : base(options) 
    {

    }
    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email, "idx_users_email");
        });
        base.OnModelCreating(modelBuilder);
    }
}
