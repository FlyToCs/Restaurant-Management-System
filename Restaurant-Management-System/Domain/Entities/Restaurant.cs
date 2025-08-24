namespace Restaurant_Management_System.Domain.Entities;

public class Restaurant
{
    private static int _id;
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<Food>? Menu { get; set; } = new();
    public Admin Owner{ get; set; }

    public Restaurant(string name, string description)
    {
        Id = ++_id;
        Name = name;
        Description = description;
    }
    public Restaurant(string name, string description, List<Food> menu) : this(name, description)
    {
        Menu = menu;

    }
}