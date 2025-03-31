using System.Text.Json.Serialization;
using AccidentMonitoring.Domain.Entities.MapStuff;

namespace AccidentMonitoring.Application.ORService.Queries.GetDirections;
public class GetDirectionAdvanceRequestDto
{
    [JsonPropertyName("profile")]
    public string Profile { get; set; } = "driving-car";
    /// <summary>
    /// Collection of waypoints that are desired intermediate points on the route.
    /// [
    ///     {longitude1, latitude1}, // this one is the starting point
    ///     {longitude2, latitude2}, // this one is a waypoint
    ///     ...
    ///     {longitudeN, latitudeN} // this one is the destination
    /// ]
    /// </summary>

    [JsonPropertyName("coordinates")]
    public Coordinate[] AvoidCoordinates { get; set; } = [];
    [JsonPropertyName("unit")]
    public string Unit { get; set; } = "km";

    /// <summary>
    /// The language of the route instructions. 
    /// Current avaible languages are:
    ///    ["cs", "da", "de"],
    ///    ["en", "eo", "es"],
    ///    ["fi", "fr", "gr"],
    ///    ["he", "hu", "id"],
    ///    ["it", "ja", "nb"],
    ///    ["ne", "nl", "pl"],
    ///    ["pt", "ro", "ru"],
    ///    ["tr", "ua", "vi"],
    ///    ["zh"]
    /// </summary>
    [JsonPropertyName("language")]
    public string Language { get; set; } = "en";


    /// <summary>
    /// This is the routing options.With the request body parameter options, 
    /// advanced routing options can be specified for a directions request.
    /// Some available options are:
    ///     - avoid_polygons: Avoid certain polygons which using the GeoJson format. 
    ///         Learn more here https://datatracker.ietf.org/doc/html/rfc7946#appendix-A.6 
    ///     - avoid_features: Avoid certain features 
    ///         ["highways", "tollways", "ferries", "fords", "steps"]
    ///         (NOT IMPLEMENTED YET)
    ///     - avoid_borders: Avoid crossing borders. 
    ///         (NOT IMPLEMENTED YET)
    ///     - avoid_countries: Avoid certain countries. 
    ///         Which can be found here https://giscience.github.io/openrouteservice/technical-details/country-list
    ///         (NOT IMPLEMENTED YET)
    ///     
    /// </summary>
    [JsonPropertyName("options")]
    public ORSDirectionOptionsDto? RoutingOptions { get; set; }
}
