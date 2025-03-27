using MediatR;
using UsersApi.Model;

namespace UsersApi.Commands.OtpRequest;

public class OtpRequestCommand
    : IRequest<ResultModel<OtpRequestCommandDto>>
{
    public string Email { get; set; } = null!;
}
