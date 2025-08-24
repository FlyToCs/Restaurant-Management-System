using Restaurant_Management_System.Domain.Enums;

namespace Restaurant_Management_System.Domain.Entities;

public abstract class Food
{
    private static int _idSet;
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public FoodTypeEnum Type { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }

    protected Food(string name, FoodTypeEnum type, decimal price)
    {
        Id = ++_idSet;
        Name = name;
        Type = type;
        Price = price;
    }


}