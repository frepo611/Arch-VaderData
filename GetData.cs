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
    internal class GetData
    {
        private static string tempData = "tempdata5-med fel.txt";

        private static string CheckFormat()
        {
            while (true)
            {
                Console.WriteLine("Format 2016-MM-DD");
                Console.Write("Enter date: ");
                string regxDate = "2016-(?!(1[3-9]))[0-1][1-9]-(?:0[1-9]|[12][0-9]|3[01])";
                string date = Console.ReadLine();

                Regex regex = new Regex(regxDate);
                MatchCollection matches = regex.Matches(date);

                if (matches.Count == 0)
                {
                    Console.WriteLine("Wrong format");
                }

                else
                {
                    return date;
                }

            }
        }

        public static void AverageTempAndHumidity()
        {

            string dateToCheck = "^";
            dateToCheck += CheckFormat();
            dateToCheck += ".*?,Ute,([\\d.]+),([\\d.]+)";
            double averageTemp = 0;
            double averageHumidity = 0;


            using (StreamReader reader = new StreamReader(tempData))
            {

                RegexOptions options = RegexOptions.Multiline;
                string fullData = reader.ReadToEnd();
                Regex data = new Regex(dateToCheck, options);
                MatchCollection matches = data.Matches(fullData);

                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        averageTemp += double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                        averageHumidity += double.Parse(match.Groups[2].Value);
                    }
                }              

                averageTemp = Math.Round(averageTemp / matches.Count(),2);
                averageHumidity = Math.Round(averageHumidity / matches.Count(), 0);
            }

            Console.WriteLine($"Average temp: {averageTemp}");
            Console.WriteLine($"Average humidity: {averageHumidity}");
        }

    }
}
