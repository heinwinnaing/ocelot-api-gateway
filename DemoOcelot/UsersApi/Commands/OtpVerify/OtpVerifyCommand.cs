using MediatR;
using UsersApi.Model;

namespace UsersApi.Commands.OtpVerify;

public class OtpVerifyCommand
    : IRequest<ResultModel<OtpVerifyCommandDto>>
{
    public Guid Token { get; set; }
    public string Code { get; set; } = null!;
}
