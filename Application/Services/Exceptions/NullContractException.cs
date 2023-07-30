namespace Services.Exceptions;

public class NullContractException : Exception
{
    public NullContractException() { }
    public NullContractException(string message) : base(message) { }
    public NullContractException(string message, Exception ex) : base(message, ex) { }
}
