using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Enums;
using Restaurant_Management_System.Domain.Interfaces;
using Restaurant_Management_System.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Application.Services
{
    public class AdminService : IAdminService
    {
        public void AddRestaurant(Restaurant restaurant, Admin admin)
        {
            admin.Restaurants.Add(restaurant);
            Storage.RestaurantList.Add(restaurant);
        }

        public List<Restaurant> GetAllRestaurants(Admin admin)
        {
            return admin.Restaurants;
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

        public Restaurant GetRestaurant(Admin admin, int restaurantId)
        {
            foreach (var restaurant in admin.Restaurants)
            {
                if (restaurant.Id == restaurantId)
                {
                    return restaurant;
                }
            }

            throw new Exception("the admin didn't have a restaurant with that id");
        }


        public Restaurant ChangeRestaurant(Restaurant restaurant)
        {
            return Storage.CurrentRestaurant = restaurant;
        }

        public void AddCustomer(Customer customer)
        {
            Storage.UserList.Add(customer);
        }
        public bool RemoveCustomer(Customer customer)
        {
            Storage.UserList.Remove(customer);
            return true;
        }

        public Customer SearchCustomer(int customerId)
        {
            foreach (var user in Storage.UserList)
            {
                if (user.Id == customerId && user.Role == RoleEnum.Customer)
                {
                    return (Customer)user;
                }
            }
            return null!;
        }

        public List<Customer> GetAllCustomers(List<User> users)
        {
            List<Customer> customerList = new List<Customer>();
            foreach (var user in users)
            {
                if (user.Role == RoleEnum.Customer)
                {
                    customerList.Add((Customer)user);
                }
            }

            return customerList;
        }

    }
}
