using Restaurant_Management_System.Domain.Entities;

namespace Restaurant_Management_System.Domain.Interfaces;

public interface IRestaurantService
{
    // Food operations
    void AddFood(Food food);
    bool RemoveFood(Restaurant restaurant, Food food);
    bool UpdateFood(Food food);
    List<Food>? ShowMenu(Restaurant restaurant);

    Food SearchFood(Restaurant restaurant,int id);



}