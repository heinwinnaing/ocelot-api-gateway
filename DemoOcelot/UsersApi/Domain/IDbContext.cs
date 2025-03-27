using Microsoft.EntityFrameworkCore;
using UsersApi.Domain.Users;

namespace UsersApi.Domain;

public interface IDbContext
{
    DbSet<User> Users { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
