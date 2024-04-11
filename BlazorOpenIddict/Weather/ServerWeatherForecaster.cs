using BlazorOpenIddict.Client;

namespace BlazorOpenIddict.Weather;

public class ServerWeatherForecaster() : IWeatherForecaster
{
    public readonly string[] summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        // Simulate asynchronous loading to demonstrate streaming rendering
        await Task.Delay(5000);

        return Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();
    }
}