using Microsoft.EntityFrameworkCore;
using UsersApi.Domain;
using UsersApi.Extensions;
using UsersApi.Handlers;
using UsersApi.Persistence;
using UsersApi.Services;

var builder = WebApplication.CreateBuilder(args);

#region #database
builder.Services.AddDbContext<EFContent>(options => 
{
    options.UseInMemoryDatabase("books_inMemoryDb");
}).AddScoped<IDbContext, EFContent>();
#endregion

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
builder.Services.AddEndpointsApiExplorer();
#endregion

builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
});
builder.Services.AddCustomValidations();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.RegisterEndpoints();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<JwtTokenService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapEndpoints();
app.UseExceptionHandler();
app.AddDemoData();
app.Run();
