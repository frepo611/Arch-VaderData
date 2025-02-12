namespace Arch_VaderData;

public class WeatherData
{
    public Dictionary<DateTime, LocationWeatherData> Data { get; }
    public Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>> AllData { get; }

    public WeatherData()
    {
        Data = new Dictionary<DateTime, LocationWeatherData>();
        AllData = new Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>>();
    }

    public void AverageData()
    {
        foreach (var entry in AllData)
        {
            var dateTime = entry.Key;
            var dataEntry = entry.Value;

            double? avgIndoorTemperature = null;
            double? avgIndoorHumidity = null;
            double? avgOutdoorTemperature = null;
            double? avgOutdoorHumidity = null;

            if (dataEntry.ContainsKey("Inne"))
            {
                var indoorData = dataEntry["Inne"];
                if (indoorData.Temperatures.Count = 0)
                avgIndoorTemperature = indoorData.Temperatures.Average();
                avgIndoorHumidity = indoorData.Humidities.Average();
            }

            if (dataEntry.ContainsKey("Ute"))
            {
                var outdoorData = dataEntry["Ute"];
                avgOutdoorTemperature = outdoorData.Temperatures.Average();
                avgOutdoorHumidity = outdoorData.Humidities.Average();
            }

            DailyAverage indoorWeatherData = new DailyWeatherData(avgIndoorTemperature, avgIndoorHumidity);
            DailyAverage outdoorWeatherData = new DailyWeatherData(avgOutdoorTemperature, avgOutdoorHumidity);
            LocationWeatherData locationWeatherData = new LocationWeatherData(indoorWeatherData, outdoorWeatherData);

            Data[dateTime] = locationWeatherData;
        }
    }

    public void AddDataLine(string dataLine, int index)
    {
        string[] data = dataLine.Split(',');
        DateTime dateTime = ParseDate(data[0], index);
        string location = data[1];
        double temperature = ParseDouble(data[2], "temperature", index);
        double humidity = ParseDouble(data[3], "humidity", index);
        AddData(dateTime, location, temperature, humidity);
    }

    private DateTime ParseDate(string dateStr, int index)
    {
        if (DateTime.TryParse(dateStr, out DateTime dateTime))
        {
            return dateTime.Date;
        }
        else
        {
            Console.WriteLine($"Unable to parse date: {dateStr} at line {index}");
            return default;
        }
    }

    private double ParseDouble(string doubleStr, string fieldName, int index)
    {
        if (double.TryParse(doubleStr, out double value))
        {
            return value;
        }
        else
        {
            Console.WriteLine($"Unable to parse {fieldName}: {doubleStr} at line {index}");
            return default;
        }
    }

    private void AddData(DateTime dateTime, string location, double temperature, double humidity)
    {
        if (!AllData.ContainsKey(dateTime))
        {
            AllData[dateTime] = new Dictionary<string, (List<double> Temperatures, List<double> Humidities)>
                {
                    { "Inne", (new List<double>(), new List<double>()) },
                    { "Ute", (new List<double>(), new List<double>()) }
                };
        }

        var dataEntry = AllData[dateTime][location];
        dataEntry.Temperatures.Add(temperature);
        dataEntry.Humidities.Add(humidity);
        AllData[dateTime][location] = dataEntry;
    }
    public Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>> GetAllData()
    {
        return AllData;
    }
}
