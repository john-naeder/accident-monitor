using System.Text.Json.Serialization;

namespace AccidentMonitoring.Application.ORService.Queries.GetDirections;

// Todo: Document these classes' properties
public class GetDirectionDefaultResponseDto
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("bbox")]
    public List<float> Bbox { get; set; } = [];

    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; } = [];

    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; set; } = new Metadata();
}

public class Feature
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("bbox")]
    public List<double> Bbox { get; set; } = [];

    [JsonPropertyName("properties")]
    public Properties Properties { get; set; } = new Properties();

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; } = new Geometry();
}

public class Properties
{
    [JsonPropertyName("segments")]
    public List<Segment> Segments { get; set; } = [];

    [JsonPropertyName("way_points")]
    public List<int> WayPoints { get; set; } = [];

    [JsonPropertyName("summary")]
    public Summary Summary { get; set; } = new Summary();
}

public class Segment
{
    [JsonPropertyName("distance")]
    public double Distance { get; set; }

    [JsonPropertyName("duration")]
    public double Duration { get; set; }

    [JsonPropertyName("steps")]
    public List<Step> Steps { get; set; } = [];
}

public class Step
{
    [JsonPropertyName("distance")]
    public double Distance { get; set; }

    [JsonPropertyName("duration")]
    public double Duration { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("instruction")]
    public string Instruction { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("way_points")]
    public List<int> WayPoints { get; set; } = [];
}

public class Summary
{
    [JsonPropertyName("distance")]
    public double Distance { get; set; }

    [JsonPropertyName("duration")]
    public double Duration { get; set; }
}

public class Geometry
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("coordinates")]
    public List<List<double>> Coordinates { get; set; } = [];
}

public class Metadata
{
    [JsonPropertyName("attribution")]
    public string Attribution { get; set; } = string.Empty;

    [JsonPropertyName("service")]
    public string Service { get; set; } = string.Empty;

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("query")]
    public Query Query { get; set; } = new Query();

    [JsonPropertyName("engine")]
    public EngineInfoDto Engine { get; set; } = new EngineInfoDto();
}

public class Query
{
    [JsonPropertyName("coordinates")]
    public List<List<double>> Coordinates { get; set; } = [];

    [JsonPropertyName("profile")]
    public string Profile { get; set; } = string.Empty;

    [JsonPropertyName("profileName")]
    public string ProfileName { get; set; } = string.Empty;

    [JsonPropertyName("format")]
    public string Format { get; set; } = string.Empty;
}
