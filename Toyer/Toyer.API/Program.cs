using Toyer.API.Extensions.WebAppBuilder;
using Toyer.Logic.Exceptions;


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
builder.Services.AddTransient<ExceptionCustomHandler>();

//  ASP.NET Core MVC trims the suffix Async from action names by default
builder.Services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

#endregion



#region WebAppMiddleware

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionCustomHandler>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion