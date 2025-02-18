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
            

            string data = "";
            List<(string, double, double, double)> insideData = DataInOrder.AvgAMonth("Inside");
            List<(string, double, double, double)> outsideData = DataInOrder.AvgAMonth("Outside");

            MakeString(insideData,outsideData);



            Console.WriteLine(data);






        }

        private static void MakeString(List<(string, double, double, double)> inside, List<(string, double, double, double)> outside)
        {
            DateTime autumDate = GetSeason.CalculateSeason(10);
            DateTime winterDate = GetSeason.CalculateSeason(0);


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
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
            }
        }
        static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
