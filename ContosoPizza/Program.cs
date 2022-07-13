using ContosoPizza.Data;
using ContosoPizza.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddScoped<PizzaService>()
    .AddDbContext<PizzaContext>(
        b => b.UseSqlite("Data Source=ContosoPizza.db")
            .EnableSensitiveDataLogging());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PizzaContext>();
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
    await DbInitializer.InitializeAsync(context);
}

app.Run();
