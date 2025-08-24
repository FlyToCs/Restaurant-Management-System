using Restaurant_Management_System.Domain.Enums;

namespace Restaurant_Management_System.Domain.Entities;

public class Dessert(string name, FoodTypeEnum type, decimal price) : Food(name, type, price)
{
    public int SweetnessLevel { get; set; }
    public bool IsFrozen { get; set; }

    public Dessert(string name, FoodTypeEnum type, int sweetLevel,bool isFrozen, decimal price) : this(name, type, price)
    {
        IsFrozen = isFrozen;
        SweetnessLevel = sweetLevel;
    }
}