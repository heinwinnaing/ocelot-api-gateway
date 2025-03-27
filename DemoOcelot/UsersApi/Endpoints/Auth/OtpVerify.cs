
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Commands.OtpVerify;
using UsersApi.Model;

namespace UsersApi.Endpoints.Auth;

public class OtpVerify
    : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/otp-verify", async (
            [FromBody]OtpVerifyCommand command,
            CancellationToken cancellationToken,
            IMediator mediator) => 
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.BadRequest(result);
        })
            .WithTags("Authentication")
            .WithSummary("[Otp verify]")
            .HasApiVersion(1)
            .Accepts<OtpVerifyCommand>("application/json")
            .Produces<ResultModel<OtpVerifyCommandDto>>(200, "application/json");
    }
}
