
namespace Services.Exceptions;

public class IncorrectEmployeeException : Exception
{
    public IncorrectEmployeeException() { }
    public IncorrectEmployeeException(string message) : base(message) { }
    public IncorrectEmployeeException(string message, Exception ex) : base(message, ex) { }
}
