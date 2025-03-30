using System.Net;
using System.Text.Json;
using AccidentMonitoring.Application.Common.Exceptions;
using AccidentMonitoring.Application.Common.Interfaces;
using AccidentMonitoring.Application.ORService.Queries.GetDirections;

namespace AccidentMonitoring.Infrastructure.ORS;
public class ORService(ApiConfiguration options) : IORService
{
    private readonly HttpClient _httpClient = new();

    public async Task<TResponse> GetStatus<TResponse>()
    {
        var response = await _httpClient.GetAsync($"{options.Uri}/status");
        if (!response.IsSuccessStatusCode)
        {
            throw new ServiesUnavaileException();
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TResponse>(responseContent);

        return result!;
    }

    public async Task<TResponse> HealthCheck<TResponse>()
    {
        var response = await _httpClient.GetAsync($"{options.Uri}/health");
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ServiesUnavaileException();
        }
        var result = JsonSerializer.Deserialize<TResponse>(responseContent);
        return result!;
    }

    public async Task<TResponse> GetDefaultRoutingDirectionAsync<TResponse>(string profile, GetDirectionDefaultRequestDto request)
    {
        var url = $"{options.Uri}/directions/{profile}?start={Uri.EscapeDataString(request.StartingCoordinate)}&end={Uri.EscapeDataString(request.DestinationCoordinate)}";
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await _httpClient.SendAsync(requestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ServiesUnavaileException();
        }

        var result = JsonSerializer.Deserialize<TResponse>(responseContent);
        return result!;
    }
}
