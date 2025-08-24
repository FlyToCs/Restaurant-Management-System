using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Infrastructure.Exceptions
{
    public class UserNotFoundException: ValidationException
    {
        public UserNotFoundException()
        {
            
        }

        public UserNotFoundException(string message) : base(message)
        {
            
        }
    }
}
