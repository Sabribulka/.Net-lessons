using WebApplication1.Models;

namespace WebApplication1.Services;

public class WeatherService : IWeatherService
{
    private static readonly string[] Summaries = 
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public Guid Id { get; }

    public WeatherForecast[] GetData(int? days)
    {
        Console.WriteLine(Id);
        
        return Enumerable.Range(1, days ?? 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
            .ToArray();
    }

    public WeatherService()
    {
        Id = Guid.NewGuid();
        Console.WriteLine(Id);
    }
}
