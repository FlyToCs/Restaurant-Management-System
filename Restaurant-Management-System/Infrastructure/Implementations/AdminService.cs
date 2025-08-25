using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_System.Application.Interfaces;
using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Infrastructure.Persistence;

namespace Restaurant_Management_System.Infrastructure.Implementations
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
    }
}
