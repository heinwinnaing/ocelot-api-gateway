using MediatR;
using Microsoft.EntityFrameworkCore;
using UsersApi.Domain;
using UsersApi.Model;

namespace UsersApi.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(
    UpdateProfileCommandValidator validator,
    IDbContext dbContext)
    : IRequestHandler<UpdateProfileCommand, ResultModel>
{
    public async Task<ResultModel> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(s => s.ErrorMessage)
                .ToArray();
            return ResultModel.Error(400, errors);
        }

        var user = await dbContext
            .Users
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (user is null)
            return ResultModel.Error(400, "Unable to update profile");

        user.Name = request.Name;
        user.UpdatedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);

        return ResultModel.Success();
    }
}