using System.Text.Json.Serialization;

namespace AccidentMonitoring.Application.Accident.Dtos;
public class AccidentReportDto
{
    [JsonPropertyName("vehicle_id")]
    public required string VehicleId { get; set; }
    [JsonPropertyName("vehicle_type")]
    public string VehicleType { get; set; } = "Car";
    [JsonPropertyName("time")]
    public required DateTime Time { get; set; }
    [JsonPropertyName("lat")]
    public float Latitude { get; set; }
    [JsonPropertyName("long")]
    public float Longitude { get; set; }

}
