using Asp.Versioning.Builder;
using BooksApi.Commands.AddBook;
using BooksApi.Domain;
using BooksApi.Domain.Authors;
using BooksApi.Domain.Books;
using BooksApi.Endpoints;
using BooksApi.Queries.GetBookList;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Principal;

namespace BooksApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddBookCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<GetBookListQueryValidator>();
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
            _= dbContext ?? throw new NullReferenceException(nameof(dbContext));

            var author = new Author
            {
                Id = new Guid("530a6daf-09f7-11f0-a1c1-065030676e55"),
                Name = "William"
            };
            dbContext.Authors.Add(author);
            var book = new Book
            {
                Id = Guid.NewGuid(),
                AuthorId = new Guid("530a6daf-09f7-11f0-a1c1-065030676e55"),
                Title = "Ocelot",
                Description = "Ocelot is an open-source API Gateway designed for .NET applications, particularly those using microservices or service-oriented architecture.",
            };
            dbContext.Books.Add(book);

            dbContext.SaveChanges();
        }
    }
}
