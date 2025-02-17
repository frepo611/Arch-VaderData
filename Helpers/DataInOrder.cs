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
            if (Models.WeatherData.Data.TryGetValue(dateTime, out var dayData))
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


    public static List<(string, double, double)> AvgAMonth(string location)
    {
        DateTime dateTime = new DateTime(2016, 06, 01);

        List<(DateTime date, double temp, double humi)> allDayData = DataForSorting(location);
        List<(string month, double temp, double humi)> allMonthData = new List<(string date, double temp, double humi)>();

        while (dateTime.Year != 2017)
        {
            var monthData = allDayData
                .Where(day => day.date.Month == dateTime.Month);

            string month = dateTime.ToString("MMMM");
            double temp = 0;
            double humi = 0;

            foreach (var day in monthData)
            {
                temp += day.temp;
                humi += day.humi;
            }

            temp = Math.Round(temp / monthData.Count(), 2);
            humi = Math.Round(humi / monthData.Count(), 0);

            allMonthData.Add((month, temp, humi));

            dateTime = dateTime.AddMonths(1);
        }

        return allMonthData;
    }
}
