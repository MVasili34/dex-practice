
namespace Services.Exceptions;

public class Below18Exception : Exception
{
    public Below18Exception() { }
    public Below18Exception(string message) : base(message) { }
    public Below18Exception(string message, Exception ex) : base(message, ex) { }
}
