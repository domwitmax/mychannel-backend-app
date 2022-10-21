using Application.Interfaces.Services;
using Application.Models.History;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Minimal_api.Requests
{
    public static class HistoryRequest
    {
        public static WebApplication RegisterHistoryEndpoints(this WebApplication app)
        {
            app.MapPost("api/History", SaveVideoStatus)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithTags("History")
                .RequireAuthorization();
            app.MapGet("api/History/{videoId}", GetVideoStatus)
                .Produces<VideoStatusDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("History")
                .RequireAuthorization();

            return app;
        }
        public static IResult SaveVideoStatus(ClaimsPrincipal user, [FromServices] IHistoryService historyService, [FromBody] VideoStatusDto videoStatusDto)
        {
            string? userIdStr = user.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null)
                return Results.Unauthorized();
            if (!int.TryParse(userIdStr, out var userId))
                return Results.Unauthorized();
            bool result = historyService.SaveVideoStatus(videoStatusDto,userId);
            if (!result)
                return Results.BadRequest();
            return Results.Ok();
        }
        public static IResult GetVideoStatus(ClaimsPrincipal user, [FromServices] IHistoryService historyService, [FromRoute] int videoId)
        {
            string? userIdStr = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int? userId = null;
            if (userIdStr != null && int.TryParse(userIdStr, out int userIdTmp))
                userId = userIdTmp;
            VideoStatusDto? videoStatusDto = historyService.GetVideoStatus(videoId,userId);
            if (videoStatusDto == null)
                return Results.NotFound();
            return Results.Ok(videoStatusDto);
        }
    }
}
