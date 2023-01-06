using Microsoft.AspNetCore.Mvc;
using Application.Models.Account;
using Application.Interfaces.Services;
using Application.Data.Entities;
using Application.Services;
using System.Security.Claims;

namespace Minimal_api.Requests
{
    public static class AccountRequest
    {
        public static WebApplication RegisterAccountEndpoints(this WebApplication app)
        {
            app.MapPost("api/Account/Login", Login)
                .Produces<string>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithTags("Account");
            app.MapPost("api/Account/Register", Register)
                .Produces<string>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithTags("Account");
            app.MapPost("api/Account/GetUser/{userName}", GetUser)
                .Produces<GetUserDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Account")
                .RequireAuthorization();
            app.MapGet("api/Account/GetActualUser", GetActualUser)
                .Produces<GetUserDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Account")
                .RequireAuthorization();
            app.MapPut("api/Account/Update", Update)
                .Produces<GetUserDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Account")
                .RequireAuthorization();

            return app;
        }
        public static IResult Login([FromServices]IAccountService accountService, [FromBody] LoginDto loginDto)
        {
            string? jwt = accountService.Login(loginDto);
            if (jwt == null)
                return Results.BadRequest();
            return Results.Ok(jwt);
        }
        public static IResult Register([FromServices] IAccountService accountService, [FromBody] RegisterDto registerDto)
        {
            string? jwt = accountService.Register(registerDto);
            if (jwt == null)
                Results.BadRequest();
            return Results.Ok(jwt);
        }
        public static IResult GetUser([FromServices] IAccountService accountService, [FromRoute] string userName)
        {
            GetUserDto? userDto = accountService.GetUser(userName);
            if(userDto == null)
                return Results.NotFound();
            return Results.Ok(userDto);
        }
        public static IResult GetActualUser([FromServices] IAccountService accountService, ClaimsPrincipal user)
        {
            string? userStr = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (userStr == null)
                return Results.Unauthorized();
            GetUserDto? userDto = accountService.GetUser(userStr);
            if (userDto == null)
                return Results.NotFound();
            return Results.Ok(userDto);
        }
        public static IResult Update([FromServices] IAccountService accountService, [FromBody] UpdateDto updateDto)
        {
            UserDto? userDto = accountService.Update(updateDto);
            if (userDto == null)
                return Results.NotFound();
            return Results.Ok(updateDto);
        }
    }
}
