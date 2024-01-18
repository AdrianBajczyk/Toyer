using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ProductionTasks", policy => policy.RequireRole("Employee", "Administrator"));
});



//  ASP.NET Core MVC trims the suffix Async from action names by default
builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion