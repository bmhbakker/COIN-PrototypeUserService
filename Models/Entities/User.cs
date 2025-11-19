namespace PrototypeUserService.Models.Entities;

public class User
{
    public int Id { get; }
    public string Username { get; set; }

    public string? Token { get; } = null;
    public int Wins { get; private set; } = 0;
    public int Loss { get; private set; } = 0;
    public List<Card>? Cards { get; set; } = null;
    public List<Deck>? Decks { get; set; } = null;
    public int Mmr { get; private set; } = 0;

    public User(int id, string username, int wins, int loss, List<Card>? cards, List<Deck>? decks, int mmr)
    {
        Id = id;
        Username = username;
        Wins = wins;
        Loss = loss;
        Cards = cards;
        Decks = decks;
        Mmr = mmr;
    }

    public User(string username, string token)
    {
        Username = username;
        Token = token;
    }

    public User(string username)
    {
        Username = username;
    }

    public void addWin()
    {
        Wins++;
    }

    public void addLoss()
    {
        Loss++;
    }

    public void updateMmr(int mmr)
    {
        Mmr += mmr;
    }
}

