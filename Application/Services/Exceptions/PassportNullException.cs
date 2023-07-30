namespace Services.Exceptions;

public class PassportNullException : Exception
{
    public PassportNullException() { }
    public PassportNullException(string message) : base(message) { }
    public PassportNullException(string message, Exception ex) : base(message, ex) { }
}
