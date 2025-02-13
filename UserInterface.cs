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
        _dataStatus = new Window("Data status", 40, 0, Dictionary.GetDataStatus());
        _outdoorMenu = new Window("Outdoor data", 0, 0, GetMenuItems<Menues.Outdoor>());
        _indoorMenu = new Window("Indoor data", 0, 0, GetMenuItems<Menues.Indoor>());
        _meterologicalWinter = new Window("Meterological winter", 40, 6, new List<string> {"Not computed"});
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
                        _dataStatus.UpdateTextRows(Dictionary.GetDataStatus());
                        StartMenu();
                        break;
                    case Menues.Main.Outdoor_data:
                        Console.Clear();
                        _dataStatus.Draw();
                        _outdoorMenu.Draw();
                        _meterologicalWinter.Draw();
                        _meterologicalAutumn.Draw();
                        SelectOutdoorMenuItem();
                        break;
                    case Menues.Main.Indoor_data:
                        Console.Clear();
                        _dataStatus.Draw();
                        _indoorMenu.Draw();
                        _meterologicalWinter.Draw();
                        _meterologicalAutumn.Draw();
                        SelectIndoorMenuItem();
                        break;
                    case Menues.Main.Write_file:
                        break;
                }
            }
        }
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
                    case Menues.Indoor.Search_date:
                        AvgTemps.AvgTempDay("Inside");
                        break;
                    case Menues.Indoor.Show_warmest_to_coldest:
                        DataInOrder.TempOrHumidInOrder("Inside", false);
                        break;
                    case Menues.Indoor.Show_driest_to_most_humid:
                        DataInOrder.TempOrHumidInOrder("Inside", true);
                        break;
                    case Menues.Indoor.Show_mold_risk:
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
                    case Menues.Outdoor.Search_date:
                        var data = AvgTemps.AvgTempDay("Outside");
                        Console.WriteLine(data);
                        break;
                    case Menues.Outdoor.Show_warmest_to_coldest:
                        DataInOrder.TempOrHumidInOrder("Outside", false);
                        break;
                    case Menues.Outdoor.Show_driest_to_most_humid:
                        DataInOrder.TempOrHumidInOrder("Outside", true);
                        break;
                    case Menues.Outdoor.Meterological_autumn:
                        DateTime höst = GetSeason.CalculateSeason(10);
                        break;
                    case Menues.Outdoor.Meterological_winter:
                        DateTime vinter = GetSeason.CalculateSeason(0);
                        break;
                }
            }
        }
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
}
