namespace WebApplication1.Helpers;

// Source - https://stackoverflow.com/a/67958823
// Posted by FRL
// Retrieved 2026-10-12, License - CC BY-SA 4.0

public static class DateTimeExtensions
{
    public static DateTime ToUtc(this DateTime value)
    {
        if (value.Kind == DateTimeKind.Unspecified)
        {
            value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        else if (value.Kind == DateTimeKind.Local)
        {
            value = value.ToUniversalTime();
        }
        return value;
    }
    public static DateTime? ToUtc(this DateTime? value)
    {
        if (value.HasValue)
        {
            return ToUtc(value.Value);
        }
        return value;
    }
}
