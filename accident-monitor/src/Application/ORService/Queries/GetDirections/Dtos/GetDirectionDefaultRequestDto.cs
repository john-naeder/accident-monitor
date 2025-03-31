using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AccidentMonitoring.Application.ORService.Queries.GetDirections.Dto;
public class GetDirectionDefaultRequestDto : IParsable<GetDirectionDefaultRequestDto>
{
    /// <summary>
    /// Starting coordinate of the route in "longitude, latitude" format.
    /// </summary>
    [JsonPropertyName("start")]
    public string StartingCoordinate { get; set; } = string.Empty;

    /// <summary>
    /// Destination coordinate of the route in "longitude, latitude" format.
    /// </summary>
    [JsonPropertyName("end")]
    public string DestinationCoordinate { get; set; } = string.Empty;

    public static GetDirectionDefaultRequestDto Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var result) ? result : throw new FormatException("Input string was not in a correct format.");
    }

    public static bool TryParse(string? s, IFormatProvider? provider, [NotNullWhen(true)] out GetDirectionDefaultRequestDto? result)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        try
        {
            result = JsonSerializer.Deserialize<GetDirectionDefaultRequestDto>(s);
            return result != null;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
