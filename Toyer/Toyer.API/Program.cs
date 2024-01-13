using Toyer.API.Extensions.Exceptions;
using Toyer.API.Extensions.WebAppBuilder;



#region WebAppBuilder

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwaggerGen();
builder.Services.AddCustomRepositories();
builder.Services.AddCustomAzureServices();
builder.Services.AddCustomMappingServices();
builder.Services.AddCustomDbContexts(builder.Configuration);
builder.Services.AddCustomAuthenticationServices(builder.Configuration);
builder.Services.AddCustomIdentity();

//  ASP.NET Core MVC trims the suffix Async from action names by default
builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

#endregion



#region WebAppMiddleware

var app = builder.Build();

app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion