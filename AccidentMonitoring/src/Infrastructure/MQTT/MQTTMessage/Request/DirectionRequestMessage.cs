using System.Text.Json.Serialization;
using AccidentMonitoring.Application.ORService.Queries.GetDirections;

namespace AccidentMonitoring.Application.ORService.MQTT.Request;
public class DirectionRequestMessage
{
    [JsonPropertyName("profile")]
    public string Profile { get; set; } = "driving-car";
    [JsonPropertyName("request-id")]
    public string RequestId { get; set; } = string.Empty;
    [JsonPropertyName("request")]
    public GetDirectionDefaultRequestDto Request { get; set; } = new GetDirectionDefaultRequestDto();
}
