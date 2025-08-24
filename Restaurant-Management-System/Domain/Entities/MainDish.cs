using Restaurant_Management_System.Domain.Enums;

namespace Restaurant_Management_System.Domain.Entities;

public class MainDish(string name, FoodTypeEnum type, decimal price) : Food(name, type, price)
{
    public bool IsSpicy { get; set; }

    public MainDish(string name, FoodTypeEnum type, bool isSpicy, decimal price) : this(name, type, price)
    {
        IsSpicy = isSpicy;
    }
}