using System.Net;
using System.Text.Json;
using AccidentMonitoring.Application.Common.Exceptions;
using AccidentMonitoring.Application.Common.Interfaces;
using AccidentMonitoring.Application.ORService.Queries.GetDirections.Dto;

namespace AccidentMonitoring.Infrastructure.ORS;
public class ORService(ORSConfiguration options) : IORService
{
    private readonly HttpClient _httpClient = new();
    private readonly string _baseUri = $"{options.Uri}".TrimEnd('/') + $":{options.Port}{options.BasePath}";

    public async Task<TResponse> GetStatus<TResponse>()
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/status");
        if (!response.IsSuccessStatusCode)
        {
            throw new ServicesUnavailableException();
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TResponse>(responseContent);

        return result!;
    }

    public async Task<TResponse> HealthCheck<TResponse>()
    {
        var response = await _httpClient.GetAsync($"{_baseUri}/health");
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ServicesUnavailableException();
        }
        var result = JsonSerializer.Deserialize<TResponse>(responseContent);
        return result!;
    }

    public async Task<TResponse> GetDefaultRoutingDirectionAsync<TResponse>(string profile, GetDirectionDefaultRequestDto request)
    {
        var url = $"{_baseUri}/directions/{profile}?start={Uri.EscapeDataString(request.StartingCoordinate)}&end={Uri.EscapeDataString(request.DestinationCoordinate)}";
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await _httpClient.SendAsync(requestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ServicesUnavailableException(url);
        }

        var result = JsonSerializer.Deserialize<TResponse>(responseContent);
        return result!;
    }
}
