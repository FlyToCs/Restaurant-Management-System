
using Restaurant_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_System.Domain.Enums;
using Restaurant_Management_System.Infrastructure.Exceptions;
using Restaurant_Management_System.Domain.Interfaces;
using Restaurant_Management_System.Infrastructure;

namespace Restaurant_Management_System.Application.Services
{
    public class Authentication : IAuthentication
    {

        public User Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException("Email is empty or invalid.");
            }

            int atCount = 0;
            foreach (char c in email)
            {
                if (c == '@') atCount++;
            }

            if (atCount != 1)
            {
                throw new InvalidEmailException("Email is invalid. It must contain exactly one '@'.");
            }

            int atIndex = email.IndexOf('@');
            if (atIndex < 1 || atIndex == email.Length - 1 || !email.Substring(atIndex + 1).Contains('.'))
            {
                throw new InvalidEmailException("Email is invalid. Must have at least one '.' after '@'.");
            }

            User foundUser = null!;
            foreach (var user in Storage.UserList)
            {
                if (user.Email != null && user.Email.ToLower() == email.ToLower())
                {
                    foundUser = user;
                    break;
                }
            }

            if (foundUser == null)
            {
                throw new UserNotFoundException("User not found in the system.");
            }

            if (foundUser.Password != password)
            {
                throw new InvalidPasswordException("Password is incorrect.");
            }

            return foundUser;
        }



        public User Register(string email, string password, RoleEnum role)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException("Email is empty or invalid.");
            }

            int atCount = 0;
            foreach (char c in email)
            {
                if (c == '@') atCount++;
            }

            if (atCount != 1)
            {
                throw new InvalidEmailException("Email is invalid. It must contain exactly one '@'.");
            }

            int atIndex = email.IndexOf('@');
            if (atIndex < 1 || atIndex == email.Length - 1 || !email.Substring(atIndex + 1).Contains('.'))
            {
                throw new InvalidEmailException("Email is invalid. Must have at least one '.' after '@'.");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                throw new InvalidPasswordException("Password is invalid. Must be at least 6 characters.");
            }

            //User newUser;




            return null!;
        }
    }
}
