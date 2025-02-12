namespace Arch_VaderData;

public readonly struct DailyAverage
{
    public double? Temperature { get; }
    public double? Humidity { get; }
    public DailyAverage(double temperature, double humidity)
    {
        Temperature = temperature;
        Humidity = humidity;
    }
}
