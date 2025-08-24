using System.Security.Principal;
using Restaurant_Management_System.Domain.Enums;

namespace Restaurant_Management_System.Domain.Entities;

public class Customer(string firstName, string lastName, string email, string password, RoleEnum role, ContactInfo contactInfo) : User(firstName, lastName, email, password, role)
{
    public ContactInfo ContactInfo { get; set; } = contactInfo;

    public Customer(string email, string password, RoleEnum role, ContactInfo contactInfo) : this(null!, null!, email, password, role, contactInfo)
    {
        
    }
}