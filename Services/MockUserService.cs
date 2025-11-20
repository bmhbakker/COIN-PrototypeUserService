using PrototypeUserService.Enums;
using PrototypeUserService.Models.Responses;
using PrototypeUserService.Models.Entities;
using Microsoft.AspNetCore.Connections.Features;

namespace PrototypeUserService.Services;

//What should we return for everything? ServiceResponse to also handle errors? 
public class MockUserService
{
    //in-code storage (mock db)
    private List<User> users = new List<User>();

    public ServiceResponse<User> GetUserByUsername(string username)
    {
        //get user by username from db
        User user = new User(1, username, 0, 0, null, null, 1000);
        return new ServiceResponse<User>(user);
    }

    public ServiceResponse<User> Register(string username, string password, string passwordConfirm)
    {
        if (username == "" || username is null)
        {
            return new ServiceResponse<User>(ErrorCode.ValueIsNull, "Username may not be empty or null");
        }
        if (checkIfUsernameExists(username))
        {
            return new ServiceResponse<User>(ErrorCode.UsernameExists, "The username is already in use.");
        }

        //Optionally replace this with a method call that checks passwords
        if (password.Length < 8)
        {
            return new ServiceResponse<User>(ErrorCode.WeakPassword, "The password is not strong enough.");
        }

        if (password != passwordConfirm)
        {
            return new ServiceResponse<User>(ErrorCode.PasswordMismatch, "Passwords do not match.");
        }

        try
        {
            User newUser = new User(username);
            users.Add(newUser);
            return new ServiceResponse<User>(newUser);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<User>(ErrorCode.DatabaseError, ex.ToString());
        }
    }

    public ServiceResponse<User> UpdateUser(string token, string? newUsername, string? newPassword, string? passwordConfirm)
    {
        if (newPassword is not null && passwordConfirm is not null)
        {
            //Optionally replace this with a method call that checks passwords
            if (newPassword.Length < 8)
            {
                return new ServiceResponse<User>(ErrorCode.WeakPassword, "The password is not strong enough.");
            }

            if (newPassword != passwordConfirm)
            {
                return new ServiceResponse<User>(ErrorCode.PasswordMismatch, "Passwords do not match.");
            }
            try
            {
                //saving password hash in DB
                return new ServiceResponse<User>("Password updated");
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User>(ErrorCode.DatabaseError, ex.ToString());
            }
        }

        //get user bij username (using GetUserByUsername)
        //since only the logged in user can change their own data (username and password) and the user gets their own user object on login we don't need to get additional 
        //details from the database (only push). If the login changes to only returning a token we need to get the user by the token here as well. This is only needed to update the
        //correct user (UPDATE where UserId == (or username since this is unique as well)
        User currentUser = new User("ThisUserUsername");
        if (newUsername is not null)
        {
            if (checkIfUsernameExists(newUsername))
            {
                return new ServiceResponse<User>(ErrorCode.UsernameExists, "The username is already in use.");
            }
            try
            {
                //Update user in the database
                currentUser.Username = newUsername;
                return new ServiceResponse<User>(currentUser);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User>(ErrorCode.DatabaseError, ex.ToString());
            }
        }
        return new ServiceResponse<User>(ErrorCode.ValueIsNull, "No values were given");
    }

    public ServiceResponse<User> Login(string username, string password)
    {
        //get hashed password from database by username
        //check if the hash and the normal password "are the same"
        if (username is not null && password is not null)
        {
            //generate token
            string token = "1234-abcd-5678";
            User loggedInUser = new User(username, token);
            return new ServiceResponse<User>(loggedInUser);
        }
        return new ServiceResponse<User>(ErrorCode.InvalidCredentials, "Invalid username password combination");
    }

    public ServiceResponse<List<Card>> GetCards(string token)
    {
        List<Card> cards = new List<Card>();
        Card newCard = new Card(1, "Boulderfist Ogre", 6, 7, 6, Tribe.Neutral, Rarity.Common);
        cards.Add(newCard);
        //Get all the cards from a user by token (a user can only see its own cards
        return new ServiceResponse<List<Card>>(cards);
    }

    public ServiceResponse<List<Deck>> GetDecks(string token)
    {
        List<Deck> decks = new List<Deck>();
        List<Card> cards = new List<Card>();
        Deck newDeck = new Deck(1, "Boulderfisting", cards);
        decks.Add(newDeck);
        //Get all the decks from a user by token (a user can only see its own decks)
        return new ServiceResponse<List<Deck>>(decks);
    }

    public ServiceResponse<List<Card>> SaveCards(string token, List<Card> cardsToSave)
    {
        //get user by token and save the cards to the db
        List<Card> cards = new List<Card>();
        //for each card in cardsToSave get cardname etc
        Card newCard = new Card("Boulderfist Ogre", 6, 7, 6, Tribe.Neutral, Rarity.Common);
        cards.Add(newCard); //<-- this is the actual update to the database. 
        return new ServiceResponse<List<Card>>(cards);
    }
    public ServiceResponse<Deck> SaveDeck(string token, string deckName, List<Card> CardsInDeck)
    {
        //get user by token and save the deck to the db
        Deck newDeck = new Deck(deckName, CardsInDeck);
        return new ServiceResponse<Deck>(newDeck);
    }

    private bool checkIfUsernameExists(string username)
    {
        //get all usernames from DB and check if it already exists
        foreach (var user in users)
        {
            if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
