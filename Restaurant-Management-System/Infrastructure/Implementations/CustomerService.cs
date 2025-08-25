using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_System.Application.Interfaces;
using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Enums;
using Restaurant_Management_System.Infrastructure.Persistence;

namespace Restaurant_Management_System.Infrastructure.Implementations
{
    public class CustomerService: ICustomerService
    {
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
        public void AddCustomer(Customer customer)
        {
            Storage.UserList.Add(customer);
        }

        public bool RemoveCustomer(Customer customer)
        {
            Storage.UserList.Remove(customer);
            return true;
        }
    }
}
