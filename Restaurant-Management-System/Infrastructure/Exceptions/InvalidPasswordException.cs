namespace Restaurant_Management_System.Infrastructure.Exceptions;

public class InvalidPasswordException : ValidationException
{
    public InvalidPasswordException()
    {
        
    }
    public InvalidPasswordException(string message) : base(message)
    {
        
    }
}