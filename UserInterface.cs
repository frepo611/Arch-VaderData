namespace Arch_VaderData;

using Arch_VaderData.Helpers;
using Models;
using System.Linq.Expressions;

public static class UserInterface
{
    private static Window _mainMenu;
    private static Window _dataStatus;
    private static Window _outsideMenu;
    private static Window _insideMenu;
    private static Window _meterologicalWinter;
    private static Window _meterologicalAutumn;
    private static bool noData = true;

    static UserInterface()
    {
        Console.CursorVisible = false;
        _mainMenu = new Window("Main menu", 0, 0, GetMenuItems<Menues.Main>());
        _dataStatus = new Window("Data status", 45, 0, WeatherData.GetDataStatus());
        _outsideMenu = new Window("Outside data", 0, 0, GetMenuItems<Menues.Outside>());
        _insideMenu = new Window("Inside data", 0, 0, GetMenuItems<Menues.Inside>());
        _meterologicalWinter = new Window("Meterological winter", 45, 6, new List<string> { "Not computed" });
        _meterologicalAutumn = new Window("Meterological autumn", 45, 12, new List<string> { "Not computed" });
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
                        try
                        {
                            GetData.ReadAllData();
                            noData = false;
                        }
                        catch
                        {
                            Console.WriteLine("Data is already in use");
                        }
                        _dataStatus.UpdateTextRows(WeatherData.GetDataStatus());
                        StartMenu();
                        break;
                    case Menues.Main.Outside_data:
                        DrawOutsideMenu();
                        break;
                    case Menues.Main.Inside_data:
                        DrawInsideMenu();
                        break;
                    case Menues.Main.Write_file:
                        CreateFile.FileCreator();

                        break;
                }
            }
        }
    }

    private static void DrawInsideMenu()
    {
        Console.Clear();
        _dataStatus.Draw();
        _insideMenu.Draw();
        _meterologicalWinter.Draw();
        _meterologicalAutumn.Draw();
        SelectInsideMenuItem();
    }

    private static void DrawOutsideMenu()
    {
        Console.Clear();
        _dataStatus.Draw();
        _outsideMenu.Draw();
        _meterologicalWinter
            .Draw();
        _meterologicalAutumn.Draw();
        SelectOutsideMenuItem();
    }

    private static void SelectInsideMenuItem()
    {
        while (true)
        {
            if (TryParseInput(out Menues.Inside choice))
            {
                switch (choice)
                {
                    case Menues.Inside.Back:
                        StartMenu();
                        break;
                    case Menues.Inside.Show_measurement_for_date:
                        if (!noData)
                        {
                            var data = AvgTemps.AvgTempDay("Inside");
                            Console.WriteLine($"{data.Temperature:f1}°C, {data.Humidity}% RH");
                            Console.WriteLine("Press key");
                            Console.ReadKey();
                            DrawInsideMenu();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("There is no data");
                            break;
                        }
                    case Menues.Inside.Show_warmest_to_coldest:
                        Console.WriteLine("Warmest to coldest days inside:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Inside", false, false), false);
                        DrawInsideMenu();
                        break;
                    case Menues.Inside.Show_driest_to_most_humid:
                        Console.WriteLine("Driest to most humid days inside:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Inside", true, false), true);
                        DrawInsideMenu();
                        break;
                }
            }
        }
    }

    private static void SelectOutsideMenuItem()
    {
        while (true)
        {
            if (TryParseInput(out Menues.Outside choice))
            {
                switch (choice)
                {
                    case Menues.Outside.Back:
                        StartMenu();
                        break;
                    case Menues.Outside.Show_measurement_for_date:
                        if (!noData)
                        {
                            var data = AvgTemps.AvgTempDay("Outside");
                            Console.WriteLine($"{data.Temperature:f1}°C, {data.Humidity}% RH");
                            Console.WriteLine("Press key");
                            Console.ReadKey();
                            DrawOutsideMenu();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("There is no data");
                            break;
                        }
                    case Menues.Outside.Show_warmest_to_coldest:
                        Console.WriteLine("Warmest to coldest days:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Outside", false, false), false);
                        DrawOutsideMenu();
                        break;
                    case Menues.Outside.Show_driest_to_most_humid:
                        Console.WriteLine("Driest to most humid days:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Outside", true, false), true);
                        DrawOutsideMenu();
                        break;
                    
                    case Menues.Outside.Show_mold_risks_lowest_to_highests:
                        Console.WriteLine("Lowest to highest moldrisk:\n");
                        ShowData(DataInOrder.TempOrHumidInOrder("Outside", false, true), true);
                        DrawOutsideMenu();
                        break;
                    case Menues.Outside.Meterological_autumn:
                        try
                        {
                            DateTime autumnDate = GetSeason.CalculateSeason(10);
                            _meterologicalAutumn.UpdateTextRows(new List<string> { autumnDate.ToString("yyyy-MM-dd") });
                            _meterologicalAutumn.Draw();
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("No data to use");
                            break;
                        }
                    case Menues.Outside.Meterological_winter:
                        try
                        {
                            DateTime winterDate = GetSeason.CalculateSeason(0);
                            _meterologicalWinter.UpdateTextRows(new List<string> { winterDate.ToString("yyyy-MM-dd") });
                            _meterologicalWinter.Draw();
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("No data to use");
                            break;
                        }
                }
            }
        }
    }

    private static void ShowData(List<(DateTime date, double temp, double humi, double mold)> list, bool willShowTemp)
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
