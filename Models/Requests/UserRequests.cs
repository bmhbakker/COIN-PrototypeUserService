using PrototypeUserService.Models.Entities;
namespace PrototypeUserService.Models.Requests;

public record getUserByUsernameRequest(string Username);
public record registerRequest(string Username, string Password, string PasswordConfirm);
public record updateUserRequest(string Token, string? Username, string? Password, string? PasswordConfirm);
public record loginUserRequest(string Username, string Password);
public record getUserCardsRequest(string Token);
public record getUserDecksRequest(string Token);
public record SaveUserCardsRequest(string Token, List<Card> CardsToSave);
public record SaveUserDeckRequest(string Token, string Name, List<Card> CardsInDeck);
