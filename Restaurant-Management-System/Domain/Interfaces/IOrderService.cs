using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_System.Domain.Entities;

namespace Restaurant_Management_System.Domain.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(Order order);
        List<Order> GetOrders(Restaurant restaurant);

        decimal CalculateTotalPrice(List<Food> foods);
        decimal CalculateTotalDiscount(List<Food> foods);
        decimal CalculateFinalPrice(List<Food> foods);
    }
}
