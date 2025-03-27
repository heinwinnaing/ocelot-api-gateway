using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region #authentication
builder.Services.AddAuthentication("AccessToken")
    .AddJwtBearer("AccessToken", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = "localhost",
            ValidAudience = "localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8x95L5fXbqSgJhwK2nobqF7lUa5MQjEmnswG"))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { code = 401, message = "Your session has expired or is invalid." });
                context.HandleResponse();
            },
            OnTokenValidated = async context =>
            {
                var claims = context?.Principal?.Claims?.ToList();
            },
            OnAuthenticationFailed = async context =>
            {
                await context.Response.WriteAsJsonAsync(new { code = 401, message = "Your session has expired or is invalid." });
            }
        };
    });
#endregion

builder
    .Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseHealthChecks("/hc");
await app.UseOcelot();
app.Run();
