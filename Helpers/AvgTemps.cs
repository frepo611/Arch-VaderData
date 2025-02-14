using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arch_VaderData.Helpers
{
    internal class AvgTemps
    {

        private static DateTime CheckFormat()
        {
            while (true)
            {
                Console.WriteLine("Format 2016-MM-DD");
                Console.Write("Enter date: ");
                string regxDate = "2016-(?!(1[3-9]))[0-1][0-9]-(?:0[1-9]|[12][0-9]|3[01])";
                string date = Console.ReadLine();

                Regex regex = new Regex(regxDate);
                MatchCollection matches = regex.Matches(date);

                if (matches.Count == 0)
                {
                    Console.WriteLine("Wrong format");
                }
                else
                {
                    Console.WriteLine($"Date: {date}");
                    return DateTime.Parse(date);
                }
            }
        }

        public static (double Temperature, double Humidity) AvgTempDay(string location)
        {
            while (true)
            {
                DateTime dateInput = CheckFormat();

                if (Models.WeatherData.Data.TryGetValue(dateInput, out var dayData))
                {
                    if (location == "Inside")
                    {
                        Models.Inside inside = dayData.Item1;
                        return (inside.AvgTemp, inside.AvgHum);
                    }
                    else
                    {
                        Models.Outside outside = dayData.Item2;
                        return (outside.AvgTemp, outside.AvgHum);
                    }
                }
                else
                {
                    Console.WriteLine("Information on that date doesn't exist, try again");
                }
            }
        }



    }
}
