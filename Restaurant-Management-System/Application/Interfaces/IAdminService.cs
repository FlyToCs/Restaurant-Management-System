using Restaurant_Management_System.Domain.Entities;

namespace Restaurant_Management_System.Application.Interfaces;

public interface IAdminService
{
    void AddRestaurant(Restaurant restaurant, Admin admin);
    List<Restaurant> GetAllRestaurants(Admin admin);
}