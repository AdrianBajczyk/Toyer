using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toyer.API.Extensions.WebAppBuilder;
using Toyer.Logic.Exceptions;
using Toyer.Logic.Services.Authorization.AuthorizationHandlers;


#region WebAppBuilder

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCustomSwaggerGen();
builder.Services.AddCustomRepositories();
builder.Services.AddCustomAzureServices();
builder.Services.AddCustomMappingServices();
builder.Services.AddCustomDbContexts(builder.Configuration);
builder.Services.AddCustomAuthenticationServices(builder.Configuration);
builder.Services.AddCustomIdentity();
builder.Services.AddCustomEmailService(builder.Configuration);

builder.Services.AddTransient<ExceptionCustomHandler>();
builder.Services.AddTransient<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalHost",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); // Allow credentials (cookies)
        });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Production", policy => policy.RequireRole("Employee", "Administrator"));
});

//  ASP.NET Core MVC trims the suffix Async from action names by default
builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddMvc()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var errorDetails = actionContext.ModelState
                .Where(entry => entry.Value.Errors.Any())
                .ToDictionary(
                    entry => entry.Key,
                    entry => entry.Value.Errors.Select(error => error.ErrorMessage).ToList()
                );

            var errorDictionary = errorDetails.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.FirstOrDefault()
            );


            var result = new ObjectResult(new
            {
                status = StatusCodes.Status422UnprocessableEntity,
                message = "Validation failed",
                error = errorDictionary,
            })
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };

            

            return result;
        };
    });

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

#endregion



#region WebAppMiddleware

var app = builder.Build();

app.UseMiddleware<ExceptionCustomHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.UseCors("AllowLocalHost");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion