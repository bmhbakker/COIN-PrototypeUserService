using Microsoft.AspNetCore.Http;
using PrototypeUserService.Services;
using PrototypeUserService.Models.Requests;
using Microsoft.AspNetCore.Identity.Data;

namespace PrototypeUserService.Endpoints;

public static class UsersEndpoints
{
    public static void Map(WebApplication app)
    {
        // POST /users/getUserByUsername
        app.MapPost("/users/getUserByUsername", (getUserByUsernameRequest? req, MockUserService users) =>
        {
            if (req is null)
            {
                return Results.BadRequest("Request body is missing");
            }

            if (string.IsNullOrEmpty(req.Username))
            {
                return Results.Problem("No username was set");
            }
            var user = users.GetUserByUsername(req.Username);
            return Results.Ok(new { user });
        });

        //POST /users/register
        app.MapPost("users/register", (registerRequest? req, MockUserService users) =>
        {
            if (req is null)
            {
                return Results.BadRequest("Request body is missing");
            }

            var user = users.Register(req.Username, req.Password, req.PasswordConfirm);
            return Results.Ok(new { user });
        });

        //POST /users/updateUser
        app.MapPost("users/updateUser", (updateUserRequest? req, MockUserService users) =>
        {
            if (req is null)
            {
                return Results.BadRequest("Request body is missing");
            }

            var token = req.Token;

            //Token validation
            if (token is null)
            {
                return Results.Unauthorized();
            }

            string newUsername = req.Username ?? "";
            string newPassword = req.Password ?? "";
            string newPasswordConfirm = req.PasswordConfirm ?? "";

            if (!string.IsNullOrEmpty(newUsername) && !string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(newPasswordConfirm))
            {
                if (newPassword == newPasswordConfirm)
                {
                    var user = users.UpdateUser(token, newUsername, newPassword, newPasswordConfirm);
                    return Results.Ok(new { user });
                }
                return Results.BadRequest("newPassword != newPasswordConfirm - edit all");
            }

            if (!string.IsNullOrEmpty(newUsername))
            {
                var user = users.UpdateUser(token, newUsername, null, null);
                return Results.Ok(new { user });
            }

            if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(newPasswordConfirm))
            {
                if (newPassword == newPasswordConfirm)
                {
                    var user = users.UpdateUser(token, null, newPassword, newPasswordConfirm);
                    return Results.Ok(new { user });
                }
                return Results.BadRequest();
            }

            return Results.BadRequest("nothing to update");
        });

        //POST /users/login
        app.MapPost("users/login", (loginUserRequest? req, MockUserService users) =>
        {
            if (req is null)
            {
                return Results.BadRequest("Request body is missing");
            }

            var user = users.Login(req.Username, req.Password);
            return Results.Ok(new { user });
        });
    }
}
