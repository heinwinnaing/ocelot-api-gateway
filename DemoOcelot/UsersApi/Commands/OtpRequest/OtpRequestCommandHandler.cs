using MediatR;
using Microsoft.EntityFrameworkCore;
using UsersApi.Domain;
using UsersApi.Model;

namespace UsersApi.Commands.OtpRequest;

public class OtpRequestCommandHandler(
    OtpRequestCommandValidator validator,
    IDbContext dbContext)
    : IRequestHandler<OtpRequestCommand, ResultModel<OtpRequestCommandDto>>
{
    public async Task<ResultModel<OtpRequestCommandDto>> Handle(OtpRequestCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(s => s.ErrorMessage)
                .ToArray();
            return ResultModel<OtpRequestCommandDto>.Error(400, errors);
        }

        var user = await dbContext
            .Users
            .FirstOrDefaultAsync(r => r.Email == request.Email, cancellationToken);
        
        return ResultModel<OtpRequestCommandDto>.Success(new OtpRequestCommandDto 
        {
            Token = user?.Id ?? Guid.Empty,
            ExpiryIn = TimeSpan.FromMinutes(5)
        });
    }
}
