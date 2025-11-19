using Microsoft.AspNetCore.Http;
using PrototypeUserService.Services;
using PrototypeUserService.Models.Requests;

namespace PrototypeUserService.Endpoints;

public static class DecksEndpoints
{
    public static void Map(WebApplication app)
    {
        //POST /users/getUserCards
        app.MapPost("/users/getUserCards", (getUserCardsRequest? req, MockUserService users) =>
        {
            if (req is null)
            {
                return Results.BadRequest("Request body is missing");
            }

            var cards = users.GetCards(req.Token);
            return Results.Ok(new { cards });
        });

        //POST /users/getUserDecks
        app.MapPost("/users/getUserDecks", (getUserDecksRequest? req, MockUserService users) =>
        {
            if (req is null)
            {
                return Results.BadRequest("Request body is missing");
            }

            var decks = users.GetDecks(req.Token);
            return Results.Ok(new { decks });
        });

        //POST /users/saveCards
        app.MapPost("/users/saveCards", (SaveUserCardsRequest? req, MockUserService users) =>
        {
            if (req is null)
            {
                return Results.BadRequest("Request body is missing");
            }

            var cards = users.SaveCards(req.Token, req.CardsToSave);
            //save cards to DB
            return Results.Ok(new { cards });
        });

        //POST /users/saveDeck
        app.MapPost("/users/saveDeck", (SaveUserDeckRequest? req, MockUserService users) =>
        {
            if (req is null)
            {
                return Results.BadRequest("Request body is missing");
            }

            var deck = users.SaveDeck(req.Token, req.Name, req.CardsInDeck);
            return Results.Ok(new { deck });
        });
    }
}

