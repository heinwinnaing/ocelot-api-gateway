using BooksApi.Domain;
using BooksApi.Extensions;
using BooksApi.Handlers;
using BooksApi.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region #api-versioning
builder.Services.AddApiVersioning(options => 
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
#endregion

#region #database
builder.Services.AddDbContext<EFContext>(options => 
{
    options.UseInMemoryDatabase("books_inMemoryDb");
}).AddScoped<IDbContext, EFContext>();
#endregion

#region #error-handling
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#endregion

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
});

builder.Services.AddCustomValidations();
builder.Services.RegisterEndpoints();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler();
app.MapEndpoints();
app.AddDemoData();
app.Run();
