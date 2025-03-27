using MediatR;
using UsersApi.Model;

namespace UsersApi.Queries.GetProfile;

public record GetProfileQuery(Guid id)
    : IRequest<ResultModel<GetProfileQueryDto>>;
