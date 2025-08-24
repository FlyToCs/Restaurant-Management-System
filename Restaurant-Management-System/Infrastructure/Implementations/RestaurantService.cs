using Restaurant_Management_System.Application.Interfaces;
using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Enums;
using Restaurant_Management_System.Infrastructure.Persistence;

namespace Restaurant_Management_System.Infrastructure.Implementations;

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

    public decimal CalculateTotalPrice(List<Food> foods)
    {
        decimal total = 0;
        foreach (var f in foods)
        {
            total += f.Price;
        }
        return total;
    }

    public decimal CalculateTotalDiscount(List<Food> foods)
    {
        decimal totalDiscount = 0;
        foreach (var f in foods)
        {
            totalDiscount += f.Discount;
        }
        return totalDiscount;
    }

    // Final price after discount
    public decimal CalculateFinalPrice(List<Food> foods)
    {
        return CalculateTotalPrice(foods) - CalculateTotalDiscount(foods);
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

    // Customer operations
    public void AddCustomer(Customer customer)
    {
        Storage.UserList.Add(customer);
    }

    public bool RemoveCustomer(Customer customer)
    {
        Storage.UserList.Remove(customer);
        return true;
    }



    public List<Customer> GetCustomers()
    {
        List<Customer> allCustomers = new();
        foreach (var user in Storage.UserList)
        {
            if (user.Role == RoleEnum.Customer)
            {
                allCustomers.Add((Customer)user);
            }
        }

        return allCustomers;
    }


    // Order operations
    public void AddOrder(Order order)
    {
        order.TotalPrice = CalculateTotalPrice(order.Food);
        order.TotalDiscount = CalculateTotalDiscount(order.Food);
        Storage.Orders.Add(order);
    }




}


