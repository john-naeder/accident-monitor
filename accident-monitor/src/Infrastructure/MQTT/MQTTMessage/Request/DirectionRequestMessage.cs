using System.Text.Json.Serialization;
using AccidentMonitor.Application.ORService.Queries.GetDirections.Dtos;

namespace AccidentMonitor.Infrastructure.MQTT.MQTTMessage.Request
{
    public class DirectionRequestMessage
    {
        [JsonPropertyName("profile")]
        public string Profile { get; set; } = "driving-car";
        [JsonPropertyName("request-id")]
        public string RequestId { get; set; } = string.Empty;
        [JsonPropertyName("request")]
        public GetDirectionRequestDto Request { get; set; } = new GetDirectionRequestDto();
    }
}
