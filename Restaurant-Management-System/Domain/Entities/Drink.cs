using Restaurant_Management_System.Domain.Enums;

namespace Restaurant_Management_System.Domain.Entities;

public class Drink(string name, FoodTypeEnum type, decimal price) : Food(name, type, price)
{
    public bool IsAlcoholic { get; set; }

    public Drink(string name, FoodTypeEnum type, bool isAlcoholic, decimal price) : this(name, type, price)
    {
        IsAlcoholic = isAlcoholic;
    }

}