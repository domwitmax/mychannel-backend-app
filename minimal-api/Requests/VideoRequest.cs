using Application.Interfaces.Services;
using Application.Models.Video;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Minimal_api.Requests
{
    public static class VideoRequest
    {
        public static WebApplication RegisterVideoEndpoints(this WebApplication app)
        {
            app.MapPost("api/Video", AddVideo)
                .Produces<int>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .RequireAuthorization();
            app.MapPost("api/Video/LoadVideo/{videoId}", LoadVideo)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status409Conflict)
                .Accepts<IFormFile>("multipart/form-data")
                .RequireAuthorization();
            app.MapPost("api/Video/LoadThumbnail/{videoId}", LoadThumbnail)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status409Conflict)
                .Accepts<IFormFile>("multipart/form-data")
                .RequireAuthorization();
            app.MapDelete("api/Video/{videoId}", DeleteVideo)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .RequireAuthorization();
            app.MapGet("api/Video/{videoId}", GetVideo)
                .Produces<FullVideoDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .AllowAnonymous();
            app.MapGet("api/video/GetAllUserVideo/{userName}", GetAllUserVideo)
                .Produces<IEnumerable<FullVideoDto>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .AllowAnonymous();

            return app;
        }
        private static int? getUserId(ClaimsPrincipal user)
        {
            if (!int.TryParse(user.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId))
                return null;
            return userId;
        }
        public static IResult AddVideo([FromServices] IVideoService videoService, [FromBody] VideoDto videoDto)
        {
            int? result = videoService.AddVideo(videoDto);
            if(result == null)
                return Results.BadRequest();
            return Results.Ok(result.Value);
        }
        public async static Task<IResult> LoadVideo(HttpRequest httpRequest,ClaimsPrincipal user, [FromServices] IVideoService videoService, [FromServices] IFileService fileService, [FromRoute] int videoId)
        {
            FullVideoDto? fullVideoDto = videoService.GetVideo(videoId);
            if (fullVideoDto == null)
                return Results.BadRequest();
            int? userId = getUserId(user);
            if (userId == null || fullVideoDto.AuthorId != userId)
                return Results.Unauthorized();
            if (fullVideoDto.VideoPath != null)
                return Results.Conflict();
            if (!httpRequest.HasFormContentType)
            {
                return Results.BadRequest();
            }
            IFormCollection form = await httpRequest.ReadFormAsync();
            IFormFile? file = form.Files["file"];
            if (file is null)
                return Results.BadRequest();
            string? path = fileService.LoadVideo(file, userId.Value);
            if (path == null)
                return Results.BadRequest();
            videoService.UpdateVideo(path, videoId);
            return Results.Ok();
        }
        public async static Task<IResult> LoadThumbnail(HttpRequest httpRequest,ClaimsPrincipal user, [FromServices] IVideoService videoService, [FromServices] IFileService fileService, [FromRoute] int videoId)
        {
            FullVideoDto? fullVideoDto = videoService.GetVideo(videoId);
            if (fullVideoDto == null)
                return Results.BadRequest();
            int? userId = getUserId(user);
            if (userId == null || fullVideoDto.AuthorId != userId)
                return Results.Unauthorized();
            if (fullVideoDto.ThumbnailPath != null)
                return Results.Conflict();
            if (!httpRequest.HasFormContentType)
            {
                return Results.BadRequest();
            }
            IFormCollection form = await httpRequest.ReadFormAsync();
            IFormFile? file = form.Files["file"];
            if (file is null)
                return Results.BadRequest();
            string? path = fileService.LoadThumbnail(file, userId.Value);
            if (path == null)
                return Results.BadRequest();
            videoService.UpdateThumbnail(path, videoId);
            return Results.Ok();
        }
        public static IResult DeleteVideo(ClaimsPrincipal user, [FromServices] IVideoService videoService,[FromRoute] int videoId)
        {
            FullVideoDto? fullVideoDto = videoService.GetVideo(videoId);
            int? userId = getUserId(user);
            if (userId == null)
                return Results.Unauthorized();
            if (fullVideoDto == null)
                return Results.NotFound();
            if (userId != fullVideoDto.AuthorId)
                return Results.Unauthorized();
            bool result = videoService.DeleteVideo(videoId);
            if(!result)
                return Results.BadRequest();
            return Results.Ok();
        }
        public static IResult GetVideo(ClaimsPrincipal user, [FromServices] IVideoService videoService, [FromServices] IRankingService rankingService, [FromRoute] int videoId)
        {
            FullVideoDto? fullVideoDto = videoService.GetVideo(videoId);
            if (fullVideoDto == null)
                return Results.NotFound();
            int? userId = getUserId(user);
            rankingService.AddView(videoId, userId);
            return Results.Ok(fullVideoDto);
        }
        public static IResult GetAllUserVideo([FromServices] IVideoService videoService, [FromRoute] string userName)
        {
            IEnumerable<FullVideoDto> videoDtos = videoService.GetAllVideo(userName);
            if (videoDtos == null)
                return Results.NotFound();
            return Results.Ok(videoDtos);
        }
    }
}
