using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arch_VaderData
{
    internal class GetData
    {
        private static string tempData = "tempdata utan fel.txt";

        public static void ReadAllData()
        {
            DateTime dateTime = new DateTime(2016, 06, 01);
            DateTime dateTimeEnd = new DateTime(2016, 12, 24);

            using (StreamReader reader = new StreamReader(tempData))
            {
                RegexOptions options = RegexOptions.Multiline;
                string fullData = reader.ReadToEnd();

                while (dateTime <= dateTimeEnd)
                {
                    double inTemp = 0;
                    double inHumi = 0;
                    double outTemp = 0;
                    double outHumi = 0;
                    int countIn = 0;
                    int countOut = 0;

                    string dateToCheck = $"{dateTime.ToString("yyyy-MM-dd")}.*,(.*),(-?[\\d.]+),([\\d.]+)";

                    Regex data = new Regex(dateToCheck, options);
                    MatchCollection matches = data.Matches(fullData);

                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Success)
                            {
                                if (match.Groups[1].Value == "Inne")
                                {                                   
                                    inTemp += double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                                    inHumi += double.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
                                    countIn++;

                                }

                                else if (match.Groups[1].Value == "Ute")
                                {
                                    outTemp += double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                                    outHumi += double.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
                                    countOut++;

                                }
                            }
                        }

                        inTemp = Math.Round(inTemp / countIn, 2);
                        inHumi = Math.Round(inHumi / countIn, 0);
                        outTemp = Math.Round(outTemp / countOut, 2);
                        outHumi = Math.Round(outHumi / countOut, 0);


                        Models.Inside inside = new Models.Inside(inTemp, inHumi);
                        Models.Outside outside = new Models.Outside(outTemp,outHumi);

                        Models.WeatherData.Data.Add(dateTime, (inside,outside));                      
                    }
                    dateTime = dateTime.AddDays(1);
                }
            }
        }
    }
}
