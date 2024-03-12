using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SimpleApi.Test;

public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task WeatherForecastReturnsObject()
    {
        // Act
        var response = await _client.GetAsync("/weatherforecast");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);

        var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Length);
    }

    [Fact]
    public void Test1()
    {
    }
}