using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.API.Repositories.Extensions;
using TodoList.API.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile("secrets.json");

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // migrate db

    var dbContext = services.GetRequiredService<IDbContextFactory<TodoListContext>>().CreateDbContext();
    await dbContext.Database.MigrateAsync();
}

app.Run();