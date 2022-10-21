using Application.Interfaces.Services;
using Application.Models.Comment;
using Microsoft.AspNetCore.Mvc;

namespace Minimal_api.Requests
{
    public static class CommentRequest
    {
        public static WebApplication RegisterCommentEndpoints(this WebApplication app)
        {
            app.MapGet("api/Comment/{videoId}", GetComments)
                .Produces<IEnumerable<CommentDto>>(StatusCodes.Status200OK);
            app.MapPost("api/Comment/{videoId}", AddComment)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .RequireAuthorization();

            return app;
        }
        public static IResult GetComments([FromServices] ICommentService commentService, [FromRoute] int videoId)
        {
            IEnumerable<CommentDto> comments = commentService.GetComments(videoId);
            return Results.Ok(comments);
        }
        public static IResult AddComment([FromServices] ICommentService commentService, [FromRoute] int videoId, [FromBody] CreatedCommentDto commentDto)
        {
            bool result = commentService.AddComment(videoId, commentDto);
            if (result)
                return Results.NoContent();
            return Results.BadRequest();
        }
    }
}
