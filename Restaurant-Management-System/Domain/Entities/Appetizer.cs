using Restaurant_Management_System.Domain.Enums;

namespace Restaurant_Management_System.Domain.Entities;

public class Appetizer(string name, FoodTypeEnum type, decimal price) : Food(name, type, price)
{
    public bool IsWarm { get; set; }

    public Appetizer(string name, FoodTypeEnum type, bool isWarm,decimal price) : this(name, type, price)
    {
        IsWarm = isWarm;
    }

}