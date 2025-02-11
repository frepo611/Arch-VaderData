using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arch_VaderData
{
    internal class AverageSetDay
    {
        private static string tempData = "tempdata5-med fel.txt";

        private static string CheckFormat()
        {
            while (true)
            {
                Console.WriteLine("Format 2016-MM-DD");
                Console.Write("Enter date: ");
                string regxDate = "2016-(?!(1[3-9]))[0-1][0-9]-(?:0[1-9]|[12][0-9]|3[01])";
                string date = Console.ReadLine();
                Console.Clear();

                Regex regex = new Regex(regxDate);
                MatchCollection matches = regex.Matches(date);

                if (matches.Count == 0)
                {
                    Console.WriteLine("Wrong format");
                }
                else
                {
                    Console.WriteLine($"Date: {date}");
                    return date;
                }
            }
        }

        public static void AverageTempAndHumidityOutside()
        {
            double averageTemp = 0;
            double averageHumidity = 0;

            using (StreamReader reader = new StreamReader(tempData))
            {
                while (true)
                {
                    string dateToCheck = "^";
                    dateToCheck += CheckFormat();
                    dateToCheck += ".*?,Ute,([\\d.]+),([\\d.]+)";

                    RegexOptions options = RegexOptions.Multiline;
                    string fullData = reader.ReadToEnd();
                    Regex data = new Regex(dateToCheck, options);
                    MatchCollection matches = data.Matches(fullData);
                    if (matches.Count == 0)
                    {
                        Console.WriteLine("No matching date was found, try again");
                    }
                    else
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Success)
                            {
                                averageTemp += double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                                averageHumidity += double.Parse(match.Groups[2].Value);
                            }
                        }

                        averageTemp = Math.Round(averageTemp / matches.Count(), 2);
                        averageHumidity = Math.Round(averageHumidity / matches.Count(), 0);
                        break;
                    }
                }
            }
            Console.WriteLine($"Average temp outside: {averageTemp}");
            Console.WriteLine($"Average humidity outside: {averageHumidity}");
        }


        public static void AverageTempInside()
        {
            double averageTemp = 0;

            using (StreamReader reader = new StreamReader(tempData))
            {
                while (true)
                {
                    string dateToCheck = "^";
                    dateToCheck += CheckFormat();
                    dateToCheck += ".*?,Inne,([\\d.]+),([\\d.]+)";

                    RegexOptions options = RegexOptions.Multiline;
                    string fullData = reader.ReadToEnd();
                    Regex data = new Regex(dateToCheck, options);
                    MatchCollection matches = data.Matches(fullData);
                    if (matches.Count == 0)
                    {
                        Console.WriteLine("No matching date was found, try again");
                    }
                    else
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Success)
                            {
                                averageTemp += double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                            }
                        }
                        averageTemp = Math.Round(averageTemp / matches.Count(), 2);
                        break;
                    }
                }
                Console.WriteLine($"Average temp inside: {averageTemp}");
            }
        }
    }
}
