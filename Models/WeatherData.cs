namespace Arch_VaderData.Models;

internal static class WeatherData
{
    public static Dictionary<DateTime, (Inside?, Outside)> Data { get; } = new Dictionary<DateTime, (Inside?, Outside?)>();
    public static List<String> GetDataStatus()
    {
        {
            //Data.Count == 0 ? "No data read" : ""
            List<String> dataStatus = new List<String>();
            dataStatus.Add($"{(Data.Count == 0 ? "No data read" : $"{Data.Count} days read")}");
            return dataStatus;
        }
    }
}
