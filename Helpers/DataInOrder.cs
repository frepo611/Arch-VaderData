namespace Arch_VaderData.Helpers;

internal class DataInOrder
{
    public static List<(DateTime date, double temp, double humi, double mold)> TempOrHumidInOrder(string location, bool humidValue, bool moldValue)
    {
        List<(DateTime date, double temp, double humi, double mold)> allDayData = DataInOrder.DataForSorting(location);
        if (humidValue)
        {
            return allDayData.OrderBy(x => x.humi).ToList();
        }
        else if (moldValue)
        {
            return allDayData.OrderBy(x => x.mold).ToList();
        }
        else
        {
            return allDayData.OrderByDescending(x => x.temp).ToList();
        }
    }
    public static List<(DateTime, double, double, double)> DataForSorting(string location)
    {
        List<(DateTime date, double temp, double humi, double mold)> allDayData = new List<(DateTime, double, double,double)>();

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
                    double dayMold = MoldRiskHeatMap.CalculateMoldRisk(dayTemp, dayHum);

                    allDayData.Add((dateTime, dayTemp, dayHum,dayMold));
                }
                else
                {
                    var outsideData = dayData.Item2;

                    double dayTemp = outsideData.AvgTemp;
                    double dayHum = outsideData.AvgHum;
                    double dayMold = MoldRiskHeatMap.CalculateMoldRisk(dayTemp, dayHum);

                    allDayData.Add((dateTime, dayTemp, dayHum,dayMold));
                }
            }
            dateTime = dateTime.AddDays(1);
        }
        return allDayData;
    }


    public static List<(string, double, double, double)> AvgAMonth(string location)
    {
        DateTime dateTime = new DateTime(2016, 06, 01);

        List<(DateTime date, double temp, double humi, double mold)> allDayData = DataForSorting(location);
        List<(string month, double temp, double humi, double mold)> allMonthData = new List<(string date, double temp, double humi, double mold)>();

        while (dateTime.Year != 2017)
        {
            var monthData = allDayData
                .Where(day => day.date.Month == dateTime.Month);

            string month = dateTime.ToString("MMMM");
            double temp = 0;
            double humi = 0;
            double mold = 0;

            foreach (var day in monthData)
            {
                temp += day.temp;
                humi += day.humi;
                mold += day.mold;
            }

            temp = Math.Round(temp / monthData.Count(), 2);
            humi = Math.Round(humi / monthData.Count(), 0);
            mold = Math.Round(mold / monthData.Count(), 0);

            allMonthData.Add((month, temp, humi,mold));

            dateTime = dateTime.AddMonths(1);
        }

        return allMonthData;
    }
}
