using Restaurant_Management_System.Domain.Entities;

namespace Restaurant_Management_System.Application.Interfaces;

public interface IRestaurantService
{
    // Food operations
    void AddFood(Food food);
    bool RemoveFood(Restaurant restaurant, Food food);
    bool UpdateFood(Food food);
    List<Food>? ShowMenu(Restaurant restaurant);

    Food SearchFood(Restaurant restaurant,int id);

    // Customer operations

    List<Customer> GetCustomers();



    // Order operations
    void AddOrder(Order order);
    List<Order> GetOrders(Restaurant restaurant);



    // Price calculations
    decimal CalculateTotalPrice(List<Food> foods);
    decimal CalculateTotalDiscount(List<Food> foods);
    decimal CalculateFinalPrice(List<Food> foods);


}