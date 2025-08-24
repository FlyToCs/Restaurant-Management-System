using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Restaurant_Management_System.Infrastructure.Exceptions
{
    public class InvalidEmailException : ValidationException
    {
        public InvalidEmailException()
        {
            
        }
        public InvalidEmailException(string message): base(message)
        {
            
        }
    }
}
