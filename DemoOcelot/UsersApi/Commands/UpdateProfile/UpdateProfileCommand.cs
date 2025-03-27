using MediatR;
using UsersApi.Model;

namespace UsersApi.Commands.UpdateProfile;

public class UpdateProfileCommand
    : IRequest<ResultModel>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
