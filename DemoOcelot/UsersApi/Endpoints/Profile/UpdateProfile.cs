
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Commands.UpdateProfile;
using UsersApi.Model;

namespace UsersApi.Endpoints.Profile;

public class UpdateProfile
    : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("profile/{id}", async (
            [FromRoute]Guid id,
            [FromBody]UpdateProfileCommand command,
            CancellationToken cancellationToken,
            IMediator mediator) =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            command.Id = id;
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result);
        })
            .WithTags("Profile")
            .WithSummary("[update profile]")
            .HasApiVersion(1)
            .Accepts<UpdateProfileCommand>("application/json")
            .Produces<ResultModel>(200, "application/json");
    }
}
