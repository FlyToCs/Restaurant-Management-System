namespace Restaurant_Management_System.Domain.Entities;

public class Order
{
    private static int _serId;
    public int Id { get; set; }
    public Customer? Customer { get; set; }
    public Restaurant? Restaurant { get; set; }
    public List<Food>? Food { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }

    public Order(Customer customer, Restaurant restaurant, List<Food> food)
    {
        Id = ++_serId;
        Customer = customer;
        Food = food;
        Restaurant = restaurant;

    }
}