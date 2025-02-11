using System;
using System.Collections.Generic;

namespace Arch_VaderData;

public class DataParser
{
    private Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>> _allData;
    private Dictionary<DateTime, Dictionary<string, (double Temperature, double Humidity)>> _data;
    public DataParser()
    {
        _allData = new Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>>();
        _data = new Dictionary<DateTime, Dictionary<string, (double Temperature, double Humidity)>>();
    }

    public void AverageData()
    {
        foreach (var entry in _allData)
        {
            var dataEntry = entry.Value;
            foreach (var location in dataEntry)
            {
                var temperatures = location.Value.Temperatures;
                var humidities = location.Value.Humidities;
                double avgTemperature = temperatures.Count > 0 ? temperatures.Average() : 0;
                double avgHumidity = humidities.Count > 0 ? humidities.Average() : 0;
                _data[entry.Key] = new Dictionary<string, (double Temperature, double Humidity)>
                {
                    { location.Key, (avgTemperature, avgHumidity) }
                };
            }
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
        if (!_allData.ContainsKey(dateTime))
        {
            _allData[dateTime] = new Dictionary<string, (List<double> Temperatures, List<double> Humidities)>
            {
                { "Inne", (new List<double>(), new List<double>()) },
                { "Ute", (new List<double>(), new List<double>()) }
            };
        }

        var dataEntry = _allData[dateTime][location];
        dataEntry.Temperatures.Add(temperature);
        dataEntry.Humidities.Add(humidity);
        _allData[dateTime][location] = dataEntry;
    }
    public Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>> GetAllData()
    {
        return _allData;
    }
    public Dictionary<DateTime, Dictionary<string, (double Temperature, double Humidity)>> GetData()
    {
        return _data;
    }
}
