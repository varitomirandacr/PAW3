namespace PAW3.Architecture.Extensions;

public static class DateTimeExtensions
{
    public static int GenerateIdFromNow(this DateTime d)
    {
        var now = DateTime.Now;
        return now.Microsecond + (now.Second - now.Minute) * 1000;
    }
}
