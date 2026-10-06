using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IWeatherService
{
     public WeatherForecast[] GetData(int? days);
}