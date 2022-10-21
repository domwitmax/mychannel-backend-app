using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Models.Subscription;
using System.Security.Claims;

namespace Minimal_api.Requests
{
    public static class SubscriptionRequest
    {
        public static WebApplication RegisterSubscriptionEndpoints(this WebApplication app)
        {
            app.MapGet("api/Subscription", GetSubscriptions)
                .Produces<IEnumerable<SubscriptionDto>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithTags("Subscription")
                .RequireAuthorization();
            app.MapGet("api/Subscription/Count/{subscriptionUserName}", GetSubscriptionsCount)
                .Produces<int>(StatusCodes.Status200OK)
                .WithTags("Subscription")
                .AllowAnonymous();
            app.MapPost("api/Subscription/AddSubscription", AddSubscription)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithTags("Subscription")
                .RequireAuthorization();
            app.MapPost("api/Subscription/RemoveSubscription", RemoveSubscription)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithTags("Subscription")
                .RequireAuthorization();

            return app;
        }
        private static string? getUserName(ClaimsPrincipal user)
        {
            string? userName = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return userName;
        }
        public static IResult GetSubscriptions(ClaimsPrincipal user, [FromServices] ISubscriptionService subscriptionService)
        {
            string? userName = getUserName(user);
            if (userName == null)
                return Results.Unauthorized();
            IEnumerable<SubscriptionDto> subscriptions = subscriptionService.GetSubscriptions(userName);
            return Results.Ok(subscriptions);
        }
        public static IResult GetSubscriptionsCount([FromServices] ISubscriptionService subscriptionService, [FromRoute] string subscriptionUserName)
        {
            int count = subscriptionService.GetSubscriptionCount(subscriptionUserName);
            return Results.Ok(count);
        }
        public static IResult AddSubscription(ClaimsPrincipal user, [FromServices] ISubscriptionService subscriptionService, [FromBody] SubscriptionDto subscriptionDto)
        {
            if (getUserName(user) == null)
                return Results.Unauthorized();
            if (getUserName(user) != subscriptionDto.UserName || subscriptionDto.UserName == subscriptionDto.SubscriptionUserName)
                return Results.BadRequest();
            bool result = subscriptionService.AddSubscription(subscriptionDto);
            if (result)
                return Results.Ok();
            return Results.BadRequest();
        }
        public static IResult RemoveSubscription(ClaimsPrincipal user, [FromServices] ISubscriptionService subscriptionService, [FromBody] SubscriptionDto subscriptionDto)
        {
            if (getUserName(user) == null)
                return Results.Unauthorized();
            if (getUserName(user) != subscriptionDto.UserName || subscriptionDto.UserName == subscriptionDto.SubscriptionUserName)
                return Results.BadRequest();
            bool result = subscriptionService.RemoveSubscription(subscriptionDto);
            if (result)
                return Results.Ok();
            return Results.BadRequest();
        }
    }
}
