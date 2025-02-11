namespace Arch_VaderData;

internal class Program
{
    private static void Main()
    {
        string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
        string parentFolder = Directory.GetParent(baseFolder).Parent.Parent.Parent.FullName;
        string filePath = Path.Combine(parentFolder, "tempdata5-med fel.txt");

        var dataParser = new DataParser();
        if (File.Exists(filePath))
        {
            var fileStreamer = new FileReader(filePath, dataParser);
            fileStreamer.ParseFile();
        }
        else
        {
            Console.WriteLine("File not found.");
        }
        var data = dataParser.GetAllData();
        foreach (var entry in data)
        {
            Console.WriteLine($"Date: {entry.Key}");
            foreach (var location in entry.Value)
            {
                Console.WriteLine($"Location: {location.Key}");
                Console.WriteLine($"Temperatures: {string.Join(", ", location.Value.Temperatures)}");
                Console.WriteLine($"Humidities: {string.Join(", ", location.Value.Humidities)}");
            }
        }
    }
}

