namespace Services.Exceptions;

public class ClientDoesntExistException : Exception
{
    public ClientDoesntExistException() { }
    public ClientDoesntExistException(string message) : base(message) { }
    public ClientDoesntExistException(string message, Exception ex) : base(message, ex) { }
}
