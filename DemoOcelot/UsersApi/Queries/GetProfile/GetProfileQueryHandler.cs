using MediatR;
using Microsoft.EntityFrameworkCore;
using UsersApi.Domain;
using UsersApi.Model;

namespace UsersApi.Queries.GetProfile;

public class GetProfileQueryHandler(
    IDbContext dbContext)
    : IRequestHandler<GetProfileQuery, ResultModel<GetProfileQueryDto>>
{
    public async Task<ResultModel<GetProfileQueryDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext
            .Users
            .FirstOrDefaultAsync(r => r.Id == request.id, cancellationToken);
        if (user is null)
            return ResultModel<GetProfileQueryDto>.Error(400, "Unable to get profile.");

        return ResultModel<GetProfileQueryDto>.Success(new GetProfileQueryDto 
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        });
    }
}
