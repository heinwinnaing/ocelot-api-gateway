
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Model;
using UsersApi.Queries.GetProfile;

namespace UsersApi.Endpoints.Profile;

public class GetProfile
    : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("profile/{id}", async (
            [FromRoute]Guid id,
            CancellationToken cancellationToken,
            IMediator mediator) =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await mediator.Send(new GetProfileQuery(id), cancellationToken);
            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result); 
        })
            .WithTags("Profile")
            .WithSummary("[Get profile]")
            .HasApiVersion(1)
            .Produces<ResultModel<GetProfileQueryDto>>(200, "application/json");
    }
}
