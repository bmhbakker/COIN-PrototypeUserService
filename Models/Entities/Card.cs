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

    public int heal(int amount)
    {
        HealthPoints += amount;
        return HealthPoints;
    }

    public int takeDamage(int amount)
    {
        HealthPoints -= amount;
        return HealthPoints;
    }
    public int buffPower(int amount)
    {
        Power += amount;
        return Power;
    }

    //Currently the original cost of the card is not saved (so ingame display is the amount that it cost when played).
    //This could give problems if there are cards such as "whenever you play a card that costs x or more" or when
    //the card is added to any players hand from the battlefield
    public int debuffPower(int amount)
    {
        Power -= amount;
        return Power;
    }

    public int increaseCost(int amount)
    {
        Cost += amount;
        return Cost;
    }

    public int decreaseCost(int amount)
    {
        Cost -= amount;
        return Cost;
    }

}