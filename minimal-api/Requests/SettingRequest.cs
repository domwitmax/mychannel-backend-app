using Microsoft.AspNetCore.Mvc;
using Application.Models.Setting;
using Application.Interfaces.Services;
using System.Security.Claims;

namespace Minimal_api.Requests
{
    public static class SettingRequest
    {
        public static WebApplication RegisterSettingEndpoints(this WebApplication app)
        {
            app.MapGet("app/Setting", GetSettings)
                .Produces<SettingDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .RequireAuthorization();
            app.MapPut("app/Setting/{userId}", UpdateSettings)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .RequireAuthorization();

            return app;
        }
        public static IResult GetSettings(ClaimsPrincipal user, [FromServices] ISettingService settingService)
        {
            string? userIdStr = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr,out int userId))
                return Results.Unauthorized();
            SettingDto? settingDto = settingService.GetSetting(userId);
            if(settingDto == null)
                return Results.NotFound();
            return Results.Ok(settingDto);
        }
        public static IResult UpdateSettings([FromServices] ISettingService settingService, [FromRoute] int userId, [FromBody] SettingDto settingDto)
        {
            bool result = settingService.UpdateSetting(userId, settingDto);
            if (result)
                return Results.NoContent();
            return Results.BadRequest();
        }
    }
}
