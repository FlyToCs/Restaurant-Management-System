using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_System.Domain.Enums;

namespace Restaurant_Management_System.Domain.Entities
{
    public abstract class User
    {
        private static int _idSet;
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public RoleEnum Role { get; set; }
        


        protected User(string firstName, string lastName, string email, string password, RoleEnum role)
        {
            Id = ++_idSet;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
        }

        protected User(string email, string password, RoleEnum role) : this(null!, null!, email, password, role)
        {

        }
    }
}
