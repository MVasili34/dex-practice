
namespace Services.Exceptions;

public class IncorrectAccountException : Exception
{
    public IncorrectAccountException() { }
    public IncorrectAccountException(string message) : base(message) { }
    public IncorrectAccountException(string message, Exception ex) : base(message, ex) { }
}
