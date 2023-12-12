using Microsoft.EntityFrameworkCore;
using Toyer.API.Extensions;
using Toyer.Data.Context;
using Toyer.Data.Mappings;
using Toyer.Logic.Services.Repositories;
using Toyer.Logic.Services.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddDbContext<ToyerDbContext>(options => options.UseSqlServer(builder.Configuration["AZURE_SQL_CONNECTIONSTRING"]));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
