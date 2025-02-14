using System.Drawing;
using System.Drawing.Imaging;

namespace Arch_VaderData;

internal static class MoldRiskHeatMap

{

    // Beräknar mögelrisken utifrån temperatur (T) och relativ luftfuktighet (RH)
    public static double CalculateMoldRisk(double T, double RH)
    {
        // Normalisera RH: vid RH ≤ 75 = 0, vid RH ≥ 100 = 1, linjär däremellan
        double RH_norm = Math.Max(0, Math.Min((RH - 75) / 25.0, 1));

        double T_factor = 0;
        if (T > 0 && T < 10)
            T_factor = T / 10.0;
        else if (T >= 10 && T <= 30)
            T_factor = 1;
        else if (T > 30 && T < 40)
            T_factor = (40 - T) / 10.0;
        else
            T_factor = 0;

        return 100 * RH_norm * T_factor;
    }

    // Mappar ett riskvärde (0-100) till en färg med en gradient:
    // 0   -> Blå
    // 50  -> Gul
    // 100 -> Röd
    public static Color MapRiskToColor(double risk)
    {
        if (risk <= 50)
        {
            double fraction = risk / 50.0;
            // Interpolera från blå (0,0,255) till gul (255,255,0)
            int r = (int)(fraction * 255);
            int g = (int)(fraction * 255);
            int b = (int)((1 - fraction) * 255);
            return Color.FromArgb(r, g, b);
        }
        else
        {
            double fraction = (risk - 50) / 50.0;
            // Interpolera från gul (255,255,0) till röd (255,0,0)
            int r = 255;
            int g = (int)((1 - fraction) * 255);
            int b = 0;
            return Color.FromArgb(r, g, b);
        }
    }
    public static void Run()
    {
        // Bildens upplösning
        int width = 800;
        int height = 600;

        // Skapa en bitmap
        using (Bitmap bmp = new Bitmap(width, height))
        {
            // Loopa igenom varje pixel
            for (int x = 0; x < width; x++)
            {
                // Mappa x-koordinaten till temperatur mellan -10 och 50
                double temperature = -10 + (x / (double)width) * 60; // 60 = 50 - (-10)
                for (int y = 0; y < height; y++)
                {
                    // Mappa y-koordinaten till relativ luftfuktighet.
                    // Vi vill ha 100% längst upp (y=0) och 75% längst ner (y=height)
                    double RH = 100 - (y / (double)height) * 25; // 25 = 100 - 75

                    // Beräkna mögelrisken
                    double risk = CalculateMoldRisk(temperature, RH);
                    // Mappa riskvärdet (0–100) till en färg
                    Color color = MapRiskToColor(risk);

                    bmp.SetPixel(x, y, color);
                }
            }

            // Spara bilden som PNG
            bmp.Save("moldRisk.png", ImageFormat.Png);
        }

        Console.WriteLine("Bild sparad som moldRisk.png");
    }
}