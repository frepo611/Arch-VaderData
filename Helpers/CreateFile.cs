using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace Arch_VaderData.Helpers
{
    internal class CreateFile
    {
        static private string localFile = "WeatherData.text";

        public static void FileCreator()
        {
            DateTime autumDate;
            DateTime winterDate;

            try
            {
                autumDate = GetSeason.CalculateSeason(10);
                winterDate = GetSeason.CalculateSeason(0);
            }
            catch
            {
                Console.WriteLine("There is no data to use");
                return;
            }
            List<(string, double, double, double)> inside = DataInOrder.AvgAMonth("Inside");
            List<(string, double, double, double)> outside = DataInOrder.AvgAMonth("Outside");


            using (StreamWriter writer = new StreamWriter(localFile, true))
            {
                writer.WriteLine($"Data registrerad [{DateTime.Now}]");
                writer.WriteLine("             [Inomhus]             [Utomhus]");

                for (int i = 0; i < inside.Count; i++)
                {
                    string monthOutside = CapitalizeFirstLetter(outside[i].Item1);

                    writer.WriteLine($"[{monthOutside}]");
                    writer.WriteLine($"Temperatur:   {inside[i].Item2}°C     \t    {outside[i].Item2}°C");
                    writer.WriteLine($"Fuktighet:      {inside[i].Item3}%                   {outside[i].Item3}%");
                    writer.WriteLine($"Mögel risk:      {inside[i].Item4}%                   {outside[i].Item4}%");
                    writer.WriteLine($"");
                }
                writer.WriteLine($"Höst började [{autumDate.ToString("yyyy-MM-dd")}]            Vinter började [{winterDate.ToString("yyyy-MM-dd")}]");
                writer.WriteLine("\n[Mögel formel]\n" +
                    "double RH_norm = Math.Max(0, Math.Min((RH - 75) / 25.0, 1));\n" +
                    "double T_factor = 0;\r\n" +
                    "if (T > 0 && T < 10)\r\n" +
                    "T_factor = T / 10.0;\r\n" +
                    "else if (T >= 10 && T <= 30)\r\n" +
                    "T_factor = 1;\r\n" +
                    "else if (T > 30 && T < 40)\r\n" +
                    "T_factor = (40 - T) / 10.0;\r\n" +
                    "else\r\n" +
                    "T_factor = 0;\r\n" +
                    "return 100 * RH_norm * T_factor; ");
                writer.WriteLine("");
                writer.WriteLine("");
            }
            Console.WriteLine("Local file was created");
        }
        static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
