using System;
using System.Collections.Generic;

namespace Arch_VaderData;

public class DataParser
{
    private Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>> _data;
    public DataParser()
    {
        _data = new Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>>();
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
        if (!_data.ContainsKey(dateTime))
        {
            _data[dateTime] = new Dictionary<string, (List<double> Temperatures, List<double> Humidities)>
            {
                { "Inne", (new List<double>(), new List<double>()) },
                { "Ute", (new List<double>(), new List<double>()) }
            };
        }

        var dataEntry = _data[dateTime][location];
        dataEntry.Temperatures.Add(temperature);
        dataEntry.Humidities.Add(humidity);
        _data[dateTime][location] = dataEntry;
    }
    public Dictionary<DateTime, Dictionary<string, (List<double> Temperatures, List<double> Humidities)>> GetData()
    {
        return _data;
    }
}
