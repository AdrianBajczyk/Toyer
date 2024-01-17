using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Toyer.Logic.Exceptions.FailResponses.Abstract;
using Toyer.Logic.Responses;

namespace Toyer.Logic.Exceptions;

public sealed class ExceptionCustomHandler(ILogger<ExceptionCustomHandler> logger) : IMiddleware
{

    private readonly ILogger<ExceptionCustomHandler> _logger = logger;

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex) 
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var responseMessage = exception.Message;

        if (context.Response.StatusCode == 500)
        {
            responseMessage = "Something went wrong...";
            _logger.LogError(exception.ToString());
        }

        await context.Response.WriteAsync(new CustomResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = responseMessage
        }.ToString());
        
    }
}
