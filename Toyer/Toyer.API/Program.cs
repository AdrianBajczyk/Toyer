using Microsoft.EntityFrameworkCore;
using Toyer.API.Extensions;
using Toyer.Data.Context;
using Toyer.Data.Mappings;
using Toyer.Logic.Mappings.UserMappings;
using Toyer.Logic.Services.Repositories.Classes;
using Toyer.Logic.Services.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<IUserControllerMapings, UserControllerMappings>();
builder.Services.AddDbContext<ToyerDbContext>(options => options.UseSqlServer(builder.Configuration["AZURE_SQL_CONNECTIONSTRING"]));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
