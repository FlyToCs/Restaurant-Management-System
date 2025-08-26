using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Enums;
using Restaurant_Management_System.Domain.Interfaces;
using Restaurant_Management_System.Infrastructure;

namespace Restaurant_Management_System.Application.Services;

public class RestaurantService : IRestaurantService
{
    // Total price of foods
    public List<Order> GetOrders(Restaurant restaurant)
    {
        List<Order> orders = new();
        foreach (var order in Storage.Orders)
        {
            if (order.Restaurant == restaurant)
            {
                orders.Add(order);
            }
        }
        return orders;
    }


    // Food operations
    public void AddFood(Food food)
    {
        if (Storage.CurrentRestaurant != null)
        {
            Storage.CurrentRestaurant.Menu?.Add(food);
        }
    }

    public bool RemoveFood(Restaurant restaurant, Food food)
    {
        if (!Storage.RestaurantList.Contains(restaurant))
        {
            throw new Exception("The restaurant not found");
        }
        foreach (var item in Storage.RestaurantList)            
        {
            if (item.Menu != null && item.Menu.Contains(food))
            {
                item.Menu.Remove(food);
                return true;
            }
        }
        throw new Exception("the food not exist in this restaurant");
    }


    public bool UpdateFood(Food food)
    {
        if (Storage.CurrentRestaurant != null)
        {
            if (Storage.CurrentRestaurant.Menu != null)
                for (int i = 0; i < Storage.CurrentRestaurant.Menu.Count; i++)
                {
                    if (Storage.CurrentRestaurant.Menu[i].Id == food.Id)
                    {
                        Storage.CurrentRestaurant.Menu[i].Name = food.Name;
                        Storage.CurrentRestaurant.Menu[i].Price = food.Price;
                        Storage.CurrentRestaurant.Menu[i].Discount = food.Discount;
                        Storage.CurrentRestaurant.Menu[i].Description = food.Description;
                        return true;
                    }
                }
        }
        return false;
    }



    public List<Food>? ShowMenu(Restaurant restaurant)
    {
        return restaurant.Menu;
    }

    public Food SearchFood(Restaurant restaurant, int id)
    {
        foreach (var food in restaurant.Menu!)
        {
            if (food.Id == id)
            {
                return food;
            }
        }

        throw new Exception("The food didn't found");
    }

 





}


