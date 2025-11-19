namespace PrototypeUserService.Models.Entities;

public class Deck
{
    public int Id { get; }
    public string Name { get; set; }
    public List<Card>? Cards { get; set; }

    public Deck(int id, string name, List<Card>? cards)
    {
        Id = id;
        Name = name;
        Cards = cards;
    }

    public Deck(string name, List<Card>? cards)
    {
        Name = name;
        Cards = cards;
    }
}