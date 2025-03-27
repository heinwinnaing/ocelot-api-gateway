
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Commands.OtpRequest;
using UsersApi.Model;

namespace UsersApi.Endpoints.Auth;

public class OtpRequest
    : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/otp-request", async (
            [FromBody]OtpRequestCommand command,
            CancellationToken cancellationToken,
            IMediator mediator) => 
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await mediator.Send(command, cancellationToken);
            return Results.Ok(result);
        })
            .WithTags("Authentication")
            .WithSummary("[Otp Request]")
            .HasApiVersion(1)
            .Accepts<OtpRequestCommand>("application/json")
            .Produces<ResultModel<OtpRequestCommandDto>>(200, "application/json");
    }
}
