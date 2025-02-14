using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_VaderData.Helpers
{
    internal class GetSeason
    {
        public static DateTime CalculateSeason(float tempToCheck)
        {
            DateTime dateTime = new DateTime(2016, 08, 01);
            DateTime dateTimeEnd = new DateTime(2016, 12, 24);
            List<(DateTime date, int points)> closestDayList = new List<(DateTime, int)>();

            while (dateTime != dateTimeEnd)
            {
                if (Models.WeatherData.Data.TryGetValue(dateTime, out var dayData))
                {
                    if (dayData.Item2.AvgTemp <= tempToCheck)
                    {
                        DateTime dateCounter = dateTime;
                        int dayTempCounter = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            var dayToCheck = Models.WeatherData.Data[dateCounter];

                            if (dayToCheck.Item2.AvgTemp <= tempToCheck)
                            {
                                dayTempCounter++;

                                if (dayTempCounter == 5)
                                {
                                    return dateTime;
                                }
                                dateCounter = dateCounter.AddDays(1);
                            }
                            else
                            {
                                break;
                            }



                            

                        }
                        closestDayList.Add((dateTime, dayTempCounter));

                        
                    }
                }

                dateTime = dateTime.AddDays(1);
            }


            var closestDayListSorted = closestDayList.OrderByDescending(x => x.points).ToList();

            DateTime closestDay = closestDayListSorted[0].date;

            return closestDay;
        }

    }
}
