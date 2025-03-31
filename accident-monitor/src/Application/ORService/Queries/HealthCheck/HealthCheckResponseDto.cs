using System.Text.Json.Serialization;

namespace AccidentMonitoring.Application.ORService.Queries.HealthCheck;
public class HealthCheckResponseDto
{
    [JsonPropertyName("status")]
    public required string? Status { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }



}
