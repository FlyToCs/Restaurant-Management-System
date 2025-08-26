using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Enums;

namespace Restaurant_Management_System.Domain.Interfaces;

public interface IAuthentication
{
    User? Login(string email, string password);
    User? Register(string email, string password, RoleEnum role);
}