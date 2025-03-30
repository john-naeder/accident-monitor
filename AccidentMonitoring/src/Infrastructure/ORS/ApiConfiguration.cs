namespace AccidentMonitoring.Infrastructure.ORS;
public class ApiConfiguration
{
    public required Uri Uri { get; set; } 
    public string ApiKey { get; set; } = string.Empty;
}
