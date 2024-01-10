using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Toyer.API.Controllers;
using Toyer.API.Extensions;
using Toyer.Data.Context;
using Toyer.Data.Entities;
using Toyer.Data.Mappings;
using Toyer.Logic.Mappings.UserMappings.classes;
using Toyer.Logic.Mappings.UserMappings.Classes;
using Toyer.Logic.Services.DeviceMessaging;
using Toyer.Logic.Services.DeviceProvisioningService;
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
        Description = "This is a training project summarizing a year of learning in the CodeCool company course. " +
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


//builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<IDeviceTypeRepository, SqlDeviceTypeRepository>();
builder.Services.AddScoped<IOrderRepository, SqlOrderRepository>();
builder.Services.AddScoped<IDeviceRepository, SqlDeviceRepository>();

builder.Services.AddScoped<IUserMapings, UserMappings>();
builder.Services.AddScoped<IDeviceTypeMappings, DeviceTypeMappings>();
builder.Services.AddScoped<IOrderMappings, OrderMappings>();
builder.Services.AddScoped<IDeviceMappings, DeviceMappings>();

builder.Services.AddSingleton<IDeviceMessageService, DeviceMessageService>();
builder.Services.AddSingleton<IDpsClient, DpsClient>();

builder.Services.AddDbContext<ToyerDbContext>(options => options.UseSqlServer(builder.Configuration["ToyerLocalConnectionstring"]));

builder.Services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(builder.Configuration["ToyerIdentityLocalConnectionstring"]));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UsersDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


//  ASP.NET Core MVC trims the suffix Async from action names by default
builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["TokenValidationParameters:Issuer"],
            ValidAudience = builder.Configuration["TokenValidationParameters:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["IssuerSigningKey"]))
        };
    });

var app = builder.Build();

app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
