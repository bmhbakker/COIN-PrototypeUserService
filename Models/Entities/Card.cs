using PrototypeUserService.Enums;

namespace PrototypeUserService.Models.Entities;

public class Card
{
    public int Id { get; }
    public string Name { get; }
    public int Power { get; private set; }
    public int HealthPoints { get; private set; }
    public int Cost { get; private set; }
    public Tribe CardTribe { get; }
    public Rarity CardRarity { get; }

    public Card(int id, string name, int power, int healthPoints, int cost, Tribe tribe, Rarity rarity)
    {
        Id = id;
        Name = name;
        Power = power;
        HealthPoints = healthPoints;
        Cost = cost;
        CardTribe = tribe;
        CardRarity = rarity;
    }

    public Card(string name, int power, int healthPoints, int cost, Tribe tribe, Rarity rarity)
    {
        Name = name;
        Power = power;
        HealthPoints = healthPoints;
        Cost = cost;
        CardTribe = tribe;
        CardRarity = rarity;
    }
}
