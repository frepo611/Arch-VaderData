namespace Arch_VaderData;

internal class Program
{
    private static void Main()
    {
        string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
        string parentFolder = Directory.GetParent(baseFolder).Parent.Parent.Parent.FullName;
        string filePath = Path.Combine(parentFolder, "tempdata5-med fel.txt");

        var weatherData = new WeatherData();
        if (File.Exists(filePath))
        {
            var fileStreamer = new FileReader(filePath, weatherData);
            fileStreamer.ParseFile();
        }
        else
        {
            Console.WriteLine("File not found.");
        }
        weatherData.AverageData();
        var data = weatherData.Data;
        foreach (var entry in data)
        {
            Console.WriteLine($"Date: {entry.Key}");
            {
                Console.WriteLine($"Innetemperatur: {entry.Value.Indoors}");
            }
        }
    }
}

