using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Toyer.Logic.Exceptions.FailResponses.Abstract;
using Toyer.Logic.Exceptions.FailResponses.Derived.User;

namespace Toyer.Logic.Exceptions
{


    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message) { }
    }

    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {

            if (authorizeResult.Forbidden)
            {
                if (authorizeResult.AuthorizationFailure != null
                    && authorizeResult.AuthorizationFailure.FailedRequirements
                        .OfType<Show404Requirement>().Any())
                {
                    throw new InvalidUserOrPasswordException();
                }
                else
                {
                    throw new ForbiddenException();
                }
            }
            else if (authorizeResult.Challenged)
            {
                throw new AuthorizationException("Unauthorized access.");
            }

            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }

    public class Show404Requirement : IAuthorizationRequirement { }
}
