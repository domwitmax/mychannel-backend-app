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
                .RequireAuthorization();
            app.MapGet("api/History/{videoId}", GetVideoStatus)
                .Produces<VideoStatusDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
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
        public static IResult GetVideoStatus([FromServices] IHistoryService historyService, [FromRoute] int videoId)
        {
            VideoStatusDto? videoStatusDto = historyService.GetVideoStatus(videoId);
            if (videoStatusDto == null)
                return Results.NotFound();
            return Results.Ok(videoStatusDto);
        }
    }
}
