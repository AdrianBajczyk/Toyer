using Toyer.API.Extensions.WebAppBuilder;
using Toyer.Logic.Exceptions;


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
builder.Services.AddCustomSecurityServices(builder.Configuration);
builder.Services.AddCustomIdentity();
builder.Services.AddCustomEmailService(builder.Configuration);
builder.Services.AddCustomMvcs();

#endregion



#region WebAppMiddleware

var app = builder.Build();


app.UseMiddleware<ExceptionCustomHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("Production");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion