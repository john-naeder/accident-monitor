namespace AccidentMonitoring.Application.Common.Exceptions;
public class ServicesUnavailableException : Exception
{
    public ServicesUnavailableException() : base() { }
    public ServicesUnavailableException(string message) : base(message) { }
}
