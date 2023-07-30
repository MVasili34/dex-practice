namespace Services.Exceptions;

public class FailedToRemoveException : Exception
{
    public FailedToRemoveException() { }
    public FailedToRemoveException(string message) : base(message) { }
    public FailedToRemoveException(string message, Exception ex) : base(message, ex) { }
}
