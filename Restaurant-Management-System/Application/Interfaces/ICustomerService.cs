using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_System.Domain.Entities;

namespace Restaurant_Management_System.Application.Interfaces
{
    public interface ICustomerService
    {
        Customer SearchCustomer(int customerId);
        List<Customer> GetAllCustomers(List<User> users);
        void AddCustomer(Customer customer);
        bool RemoveCustomer(Customer customer);
    }
}
