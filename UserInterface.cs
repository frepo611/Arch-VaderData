namespace Arch_VaderData;

using Arch_VaderData.Helpers;
using Models;
public static class UserInterface
{
    private static Window _mainMenu;
    private static Window _dataStatus;
    private static Window _outdoorMenu;
    private static Window _indoorMenu;
    private static Window _meterologicalWinter;
    private static Window _meterologicalAutumn;

    static UserInterface()
    {
        Console.CursorVisible = false;
        _mainMenu = new Window("Main menu", 0, 0, GetMenuItems<Menues.Main>());
        _dataStatus = new Window("Data status", 40, 0, WeatherData.GetDataStatus());
        _outdoorMenu = new Window("Outdoor data", 0, 0, GetMenuItems<Menues.Outdoor>());
        _indoorMenu = new Window("Indoor data", 0, 0, GetMenuItems<Menues.Indoor>());
        _meterologicalWinter = new Window("Meterological winter", 40, 6, new List<string> { "Not computed" });
        _meterologicalAutumn = new Window("Meterological autumn", 40, 12, new List<string> { "Not computed" });
    }
    public static void StartMenu()
    {
        Console.Clear();
        _mainMenu.Draw();
        _dataStatus.Draw();
        _meterologicalWinter.Draw();
        _meterologicalAutumn.Draw();
        SelectMainMenuItem();
    }

    private static void SelectMainMenuItem()
    {
        while (true)
        {
            if (TryParseInput(out Menues.Main choice))
            {
                switch (choice)
                {
                    case Menues.Main.Exit:
                        Environment.Exit(0);
                        break;
                    case Menues.Main.Read_data:
                        Console.Clear();
                        GetData.ReadAllData();
                        _dataStatus.UpdateTextRows(WeatherData.GetDataStatus());
                        StartMenu();
                        break;
                    case Menues.Main.Outdoor_data:
                        DrawOutdoorMenu();
                        break;
                    case Menues.Main.Indoor_data:
                        DrawIndoorMenu();
                        break;
                    case Menues.Main.Write_file:
                        break;
                }
            }
        }
    }

    private static void DrawIndoorMenu()
    {
        Console.Clear();
        _dataStatus.Draw();
        _indoorMenu.Draw();
        _meterologicalWinter.Draw();
        _meterologicalAutumn.Draw();
        SelectIndoorMenuItem();
    }

    private static void DrawOutdoorMenu()
    {
        Console.Clear();
        _dataStatus.Draw();
        _outdoorMenu.Draw();
        _meterologicalWinter.Draw();
        _meterologicalAutumn.Draw();
        SelectOutdoorMenuItem(); ;
    }

    private static void SelectIndoorMenuItem()
    {
        while (true)
        {
            if (TryParseInput(out Menues.Indoor choice))
            {
                switch (choice)
                {
                    case Menues.Indoor.Back:
                        StartMenu();
                        break;
                    case Menues.Indoor.Show_measurement_for_date:
                        AvgTemps.AvgTempDay("Inside");
                        break;
                    case Menues.Indoor.Show_warmest_to_coldest:
                        Console.WriteLine("Warmest to coldest days indoors:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Inside", false), false);
                        DrawIndoorMenu();
                        break;
                    case Menues.Indoor.Show_driest_to_most_humid:
                        Console.WriteLine("Driest to most humid days indoors:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Inside", true), true);
                        DrawIndoorMenu();
                        break;
                    case Menues.Indoor.Show_mold_risk_for_date:
                        break;
                    case Menues.Indoor.Show_open_balcony_door_times:
                        break;
                }
            }
        }
    }

    private static void SelectOutdoorMenuItem()
    {
        while (true)
        {
            if (TryParseInput(out Menues.Outdoor choice))
            {
                switch (choice)
                {
                    case Menues.Outdoor.Back:
                        StartMenu();
                        break;
                    case Menues.Outdoor.Show_measurement_for_date:
                        var data = AvgTemps.AvgTempDay("Outside");
                        Console.WriteLine($"{data.Temperature:f1}°C, {data.Humidity}% RH");
                        Console.WriteLine("Press key");
                        Console.ReadKey();
                        DrawOutdoorMenu();
                        break;
                    case Menues.Outdoor.Show_warmest_to_coldest:
                        Console.WriteLine("Warmest to coldest days:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Outside", false), false);
                        DrawOutdoorMenu();
                        break;
                    case Menues.Outdoor.Show_driest_to_most_humid:
                        Console.WriteLine("Driest to most humid days:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Outside", true), true);
                        DrawOutdoorMenu();
                        break;
                    case Menues.Outdoor.Show_mold_risk_for_date:
                        data = AvgTemps.AvgTempDay("Outside");
                        var moldRisk = MoldRiskHeatMap.CalculateMoldRisk(data.Temperature, data.Humidity);
                        Console.WriteLine($"Mold risk: {moldRisk}% {data.Temperature:f1}°C, {data.Humidity}% RH");
                        break;
                    case Menues.Outdoor.Meterological_autumn:
                        DateTime autumnDate = GetSeason.CalculateSeason(10);
                        _meterologicalAutumn.UpdateTextRows(new List<string> { autumnDate.ToString() });
                        _meterologicalAutumn.Draw();
                        break;
                    case Menues.Outdoor.Meterological_winter:
                        DateTime winterDate = GetSeason.CalculateSeason(0);
                        _meterologicalWinter.UpdateTextRows(new List<string> { winterDate.ToString() });
                        _meterologicalWinter.Draw();
                        break;
                }
            }
        }
    }

    private static void ShowData(List<(DateTime date, double temp, double humi)> list, bool willShowTemp)
    {
        int rowCount = 0;
        if (willShowTemp)
        {
            foreach (var date in list)
            {
                Console.Write($"{date.date.ToString("yyyy-MM-dd")}: {date.humi}%\t");
                rowCount++;
                if (rowCount == 6)
                {
                    Console.WriteLine();
                    rowCount = 0;
                }
            }
        }
        else
        {
            foreach (var date in list)
            {
                Console.Write($"{date.date.ToString("yyyy-MM-dd")}: {date.temp,-4:f1}°C ");
                rowCount++;
                if (rowCount == 6)
                {
                    Console.WriteLine();
                    rowCount = 0;
                }
            }
        }
        Console.WriteLine();
        Console.WriteLine("Press key");
        Console.ReadKey();
    }

    private static bool TryParseInput<T>(out T input) where T : struct
    {
        var rawInput = Console.ReadKey(true).KeyChar.ToString();

        return Enum.TryParse<T>(rawInput, out input);
    }
    static List<string> GetMenuItems<TEnum>() where TEnum : Enum
    {
        var results = new List<string>();
        foreach (var menuItem in Enum.GetValues(typeof(TEnum)))
        {
            results.Add($"{(int)menuItem}. {menuItem.ToString()?.Replace('_', ' ')}");
        }
        return results;
    }
    static void UpdateTextRows(this Window window, List<string> newTextRows)
    {
        window.TextRows = newTextRows;
    }
}
