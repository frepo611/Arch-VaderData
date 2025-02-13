namespace Arch_VaderData.Helpers;

internal class DataInOrder
{

    public static void TempOrHumidInOrder(string location, bool humidValue)
    {
        List<(DateTime date, double temp, double humi)> allDayData = DataInOrder.DataForSorting(location);

        int rowCount = 0;


        if (humidValue)
        {
            var sortedDataInHum = allDayData.OrderBy(x => x.humi).ToList();

            foreach (var date in sortedDataInHum)
            {
                Console.Write($"{date.date.ToString("yyyy-MM-dd")}: {date.humi}\t");
                rowCount++;
                if (rowCount == 6) { Console.WriteLine(); rowCount = 0; }
            }
        }
        else
        {
            var sortedDataInTemp = allDayData.OrderByDescending(x => x.temp).ToList();

            foreach (var date in sortedDataInTemp)
            {
                Console.Write($"{date.date.ToString("yyyy-MM-dd")}: {date.temp}\t");
                rowCount++;

                if(rowCount == 6){ Console.WriteLine(); rowCount = 0; }

            }
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
