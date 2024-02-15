using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toyer.Logic.Exceptions;

namespace Toyer.API.Extensions.WebAppBuilder;

public static class MvcServicesExtensions
{
    public static IServiceCollection AddCustomMvcs(this IServiceCollection services)
    {
        //  ASP.NET Core MVC trims the suffix Async from action names by default
        services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

        // Configure custom behavior for InvalidModelState response from actionMethod
        services.AddMvc()
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

        //Catch and handle speciffic exceptions globally
        services.AddTransient<ExceptionCustomHandler>();

        //Catch and handle authorization responses
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

        return services;
    }
}