using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UsersApi.Commands.OtpRequest;
using UsersApi.Commands.OtpVerify;
using UsersApi.Commands.UpdateProfile;
using UsersApi.Domain;
using UsersApi.Endpoints;

namespace UsersApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<OtpRequestCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<OtpVerifyCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateProfileCommand>();
        return services;
    }
    public static IServiceCollection RegisterEndpoints(this IServiceCollection services)
    {
        var serviceDescriptors = typeof(IEndpoint)
            .Assembly
            .DefinedTypes
            .Where(type => 
                type is { IsAbstract: false, IsInterface: false }
                && typeof(IEndpoint).IsAssignableFrom(type)
            )
            .Select(endpoint => ServiceDescriptor.Transient(typeof(IEndpoint), endpoint));
        services.TryAddEnumerable(serviceDescriptors);
        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new Asp.Versioning.ApiVersion(1))
            .ReportApiVersions()
            .Build();
        RouteGroupBuilder routeGroupBuilder = app.MapGroup("v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
        app
            .Services
            .GetRequiredService<IEnumerable<IEndpoint>>()
            .ToList()
            .ForEach(endpoint => endpoint.MapEndpoint(routeGroupBuilder));
        return app;
    }

    public static void AddDemoData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<IDbContext>();
            _ = dbContext ?? throw new NullReferenceException(nameof(dbContext));

            dbContext.Users.Add(new Domain.Users.User 
            {
                Id = Guid.NewGuid(),
                Name = "William",
                Email = "william@mailinator.com"
            });
            dbContext.SaveChanges();
        }
    }
}
