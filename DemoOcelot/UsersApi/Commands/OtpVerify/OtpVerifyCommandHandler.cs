using MediatR;
using Microsoft.EntityFrameworkCore;
using UsersApi.Domain;
using UsersApi.Model;
using UsersApi.Services;

namespace UsersApi.Commands.OtpVerify;

public class OtpVerifyCommandHandler(
    JwtTokenService jwtTokenService,
    OtpVerifyCommandValidator validator,
    IDbContext dbContext)
    : IRequestHandler<OtpVerifyCommand, ResultModel<OtpVerifyCommandDto>>
{
    public async Task<ResultModel<OtpVerifyCommandDto>> Handle(OtpVerifyCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(s => s.ErrorMessage)
                .ToArray();

            return ResultModel<OtpVerifyCommandDto>.Error(400, errors);
        }

        var user = await dbContext
            .Users
            .FirstOrDefaultAsync(r => r.Id == request.Token, cancellationToken);
        if (user is null
            || request.Code != DateTime.Now.ToString("yyMMdd"))
            return ResultModel<OtpVerifyCommandDto>.Error(400, "Invalid token or expired.");

        var claims = new System.Security.Claims.Claim[] 
        {
            new System.Security.Claims.Claim("id", $"{user.Id}"),
            new System.Security.Claims.Claim("email", user.Email)
        };
        var accessToken = jwtTokenService.CreateAccessToken(claims, out DateTime expiryIn);

        return ResultModel<OtpVerifyCommandDto>.Success(new OtpVerifyCommandDto 
        {
            AccessToken = accessToken,
            ExpiryIn = TimeSpan.FromMinutes(60)
        });
    }
}
