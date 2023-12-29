using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Toyer.API.Extensions;
using Toyer.Data.Context;
using Toyer.Data.Mappings;
using Toyer.Logic.Mappings.UserMappings.classes;
using Toyer.Logic.Services.Repositories.Classes;
using Toyer.Logic.Services.Repositories.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Toyer API",
        Description = "Training project summarizing a year of learning in the CodeCool company course. " +
        "Here you will find a ready-made API that supports remote control of toys powered by a microcontroller with a built-in Wi-Fi module. " +
        "Each user who has access to the aforementioned device with a unique serial code can create an account and register the device to be able to control it.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Admin",
            Email = "adrian.bajczyk@gmail.com",
        },
        License = new OpenApiLicense
        {
            Name = "What is my license",
            Url = new Uri("https://example.com/license")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<IDeviceTypeRepository, SqlDeviceTypeRepository>();
builder.Services.AddScoped<IUserControllerMapings, UserControllerMappings>();
builder.Services.AddScoped<IDeviceTypeMappings, DeviceTypeMappings>();
builder.Services.AddDbContext<ToyerDbContext>(options => options.UseSqlServer(builder.Configuration["AZURE_SQL_CONNECTIONSTRING"]));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


//  ASP.NET Core MVC trims the suffix Async from action names by default
builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

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
