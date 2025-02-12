namespace Arch_VaderData;

public readonly struct LocationWeatherData
{
    public DailyAverage? Indoors { get; }
    public DailyAverage? Outdoors { get; }
    public LocationWeatherData(DailyAverage indoors, DailyAverage outdoors)
    {
        Indoors = indoors;
        Outdoors = outdoors;
    }
}

