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
            ForbiddenException => StatusCodes.Status403Forbidden,
            AuthenticationException => StatusCodes.Status401Unauthorized,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            AuthorizationException => StatusCodes.Status401Unauthorized,
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var responseMessage = exception switch
        {
            ForbiddenException => "Forbidden",
            AuthenticationException => "Unauthorized",
            UnauthorizedAccessException => "Unauthorized",
            AuthorizationException => "Unauthorized",
            BadRequestException => "Bad Request",
            NotFoundException => "Not Found",
            _ => "Internal Server Error"
        };

        var responseError = exception.Message;

        if (context.Response.StatusCode == 500)
        {
            responseError = "Something went wrong...";
            _logger.LogError(exception.ToString());
        }


        var respnse = new CustomResponse
        {
            status = context.Response.StatusCode,
            message = responseMessage,
            error = responseError,
        }.ToString();

        await context.Response.WriteAsync(respnse);
        
    }
}
