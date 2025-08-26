using Restaurant_Management_System.Domain.Entities;

namespace Restaurant_Management_System.Domain.Interfaces;

public interface IAdminService
{
    void AddRestaurant(Restaurant restaurant, Admin admin);
    List<Restaurant> GetAllRestaurants(Admin admin);
    List<Customer> GetCustomers();
    Restaurant GetRestaurant(Admin admin, int restaurantId);
    Restaurant ChangeRestaurant(Restaurant restaurant);
    void AddCustomer(Customer customer);
    bool RemoveCustomer(Customer customer);
    Customer SearchCustomer(int customerId);
    List<Customer> GetAllCustomers(List<User> users);
}