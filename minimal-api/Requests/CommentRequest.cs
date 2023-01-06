using Application.Data.Entities;
using Application.Interfaces.Services;
using Application.Models.Comment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Minimal_api.Requests
{
    public static class CommentRequest
    {
        public static WebApplication RegisterCommentEndpoints(this WebApplication app)
        {
            app.MapGet("api/Comment/{videoId}", GetComments)
                .Produces<IEnumerable<CommentDto>>(StatusCodes.Status200OK)
                .WithTags("Comment");
            app.MapPost("api/Comment/{videoId}", AddComment)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithTags("Comment")
                .RequireAuthorization();

            return app;
        }
        private static int? getUserId(ClaimsPrincipal user)
        {
            string? userIdStr = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out int userId))
                return null;
            return userId;
        }
        public static IResult GetComments([FromServices] ICommentService commentService, [FromRoute] int videoId)
        {
            IEnumerable<CommentDto> comments = commentService.GetComments(videoId);
            return Results.Ok(comments);
        }
        public static IResult AddComment([FromServices] ICommentService commentService, [FromRoute] int videoId, [FromBody] CreatedCommentDto commentDto, ClaimsPrincipal user)
        {
            int? userId = getUserId(user);
            if (userId == null)
                return Results.Unauthorized();
            commentDto.UserId = userId.Value;
            bool result = commentService.AddComment(videoId, commentDto);
            if (result)
                return Results.NoContent();
            return Results.BadRequest();
        }
    }
}
