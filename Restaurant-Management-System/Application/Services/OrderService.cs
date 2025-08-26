using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Interfaces;
using Restaurant_Management_System.Infrastructure;

namespace Restaurant_Management_System.Application.Services
{
    public class OrderService : IOrderService
    {
        public void AddOrder(Order order)
        {
            if (order is null)
                throw new NullReferenceException("the order is null");

            Storage.Orders.Add(order);
            
        }

        public List<Order> GetOrders(Restaurant restaurant)
        {
            List<Order> orderRestaurant = new();
            foreach (Order order in Storage.Orders)
            {
                if (order.Restaurant == restaurant)
                {
                    orderRestaurant.Add(order);
                }
            }
            return orderRestaurant;
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

    }
}
