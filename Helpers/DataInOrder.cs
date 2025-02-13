namespace Arch_VaderData.Helpers;

internal class DataInOrder
{
    public static List<(DateTime date, double temp, double humi)> TempOrHumidInOrder(string location, bool humidValue)
    {
        List<(DateTime date, double temp, double humi)> allDayData = DataInOrder.DataForSorting(location);
        if (humidValue)
        {
            return allDayData.OrderBy(x => x.humi).ToList();
        }
        else
        {
            return allDayData.OrderByDescending(x => x.temp).ToList();
        }
    }
    public static List<(DateTime, double, double)> DataForSorting(string location)
    {
        List<(DateTime date, double temp, double humi)> allDayData = new List<(DateTime, double, double)>();

        DateTime dateTime = new DateTime(2016, 06, 01);
        DateTime dateTimeEnd = new DateTime(2016, 12, 24);

        while (dateTime != dateTimeEnd)
        {
            if (Models.Dictionary.Data.TryGetValue(dateTime, out var dayData))
            {
                if (location == "Inside")
                {
                    var insideData = dayData.Item1;

                    double dayTemp = insideData.AvgTemp;
                    double dayHum = insideData.AvgHum;

                    allDayData.Add((dateTime, dayTemp, dayHum));
                }
                else
                {
                    var outsideData = dayData.Item2;

                    double dayTemp = outsideData.AvgTemp;
                    double dayHum = outsideData.AvgHum;

                    allDayData.Add((dateTime, dayTemp, dayHum));
                }
            }
            dateTime = dateTime.AddDays(1);
        }
        return allDayData;
    }
}
