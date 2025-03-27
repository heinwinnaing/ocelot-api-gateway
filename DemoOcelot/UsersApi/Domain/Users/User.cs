using UsersApi.Domain.Base;

namespace UsersApi.Domain.Users;

public class User
    : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
