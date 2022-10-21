﻿using Microsoft.AspNetCore.Mvc;
using Application.Models.Video;
using Application.Interfaces.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Minimal_api.Requests
{
    public static class RankingRequest
    {
        public static WebApplication RegisterRankingEndpoints(this WebApplication app)
        {
            app.MapGet("api/Ranking/View/{videoId}", GetViews)
                .Produces<int>(StatusCodes.Status200OK)
                .AllowAnonymous();
            app.MapGet("api/Ranking/Like/{videoId}", GetLike)
                .Produces<int>(StatusCodes.Status200OK)
                .AllowAnonymous();
            app.MapPost("api/Ranking/Like/{videoId}", AddLike)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .RequireAuthorization();
            app.MapGet("api/Ranking/Dislike/{videoId}", GetDislike)
                .Produces<int>(StatusCodes.Status200OK)
                .AllowAnonymous();
            app.MapPost("api/Ranking/Dislike/{videoId}", AddDislike)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .RequireAuthorization();
            app.MapGet("api/Ranking/GetProposingVideos", GetProposingVideos)
                .Produces<IEnumerable<FullVideoDto>>(StatusCodes.Status200OK)
                .AllowAnonymous();
            app.MapGet("api/Ranking/Search/{searchKey}", Search)
                .Produces<IEnumerable<FullVideoDto>>(StatusCodes.Status200OK)
                .AllowAnonymous();
            app.MapGet("api/Ranking/IsLiked/{videoId}", IsLiked)
                .Produces<bool?>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
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
        public static IResult GetViews([FromServices] IRankingService rankingService, [FromRoute] int videoId)
        {
            int views = rankingService.GetViews(videoId);
            return Results.Ok(views);
        }
        public static IResult GetLike([FromServices] IRankingService rankingService, [FromRoute] int videoId)
        {
            int like = rankingService.GetLikes(videoId);
            return Results.Ok(like);
        }
        public static IResult AddLike(ClaimsPrincipal user, [FromServices] IRankingService rankingService, [FromRoute] int videoId)
        {
            int? userId = getUserId(user);
            if (userId == null)
                return Results.Unauthorized();
            bool like = rankingService.AddLike(videoId, userId.Value);
            if(!like)
                return Results.BadRequest();
            return Results.Ok();
        }
        public static IResult GetDislike([FromServices] IRankingService rankingService, [FromRoute] int videoId)
        {
            int dislike = rankingService.GetDislikes(videoId);
            return Results.Ok(dislike);
        }
        public static IResult AddDislike(ClaimsPrincipal user, [FromServices] IRankingService rankingService, [FromRoute] int videoId)
        {
            int? userId = getUserId(user);
            if (userId == null)
                return Results.Unauthorized();
            bool result = rankingService.AddDislike(videoId, userId.Value);
            if (!result)
                return Results.BadRequest();
            return Results.Ok();
        }
        public static IResult GetProposingVideos([FromServices] IGetVideoService getVideoService)
        {
            return Results.Ok(getVideoService.VideoProposing());
        }
        public static IResult Search([FromServices] IGetVideoService getVideoService, [FromRoute] string searchKey)
        {
            return Results.Ok(getVideoService.Search(searchKey));
        }
        public static IResult IsLiked(ClaimsPrincipal user, [FromServices] IRankingService rankingService, [FromRoute] int videoId)
        {
            int? userId = getUserId(user);
            if (userId == null)
                return Results.Unauthorized();
            bool? isLiked = rankingService.IsLiked(videoId, userId.Value);
            return Results.Ok(isLiked);
        }
    }
}
