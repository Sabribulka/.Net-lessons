using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IWeatherService
{
    Guid Id { get; }
    WeatherForecast[] GetData(int? days);
}