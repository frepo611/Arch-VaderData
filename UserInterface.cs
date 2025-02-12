namespace Arch_VaderData;
using Models;
public class UserInterface
{
    private Window _mainMenu;
    private Window _dataStatus;
    public UserInterface()
    {
        _mainMenu = new Window("Main menu", 0, 0, GetMenuItems<Menues.Main>());
        _dataStatus = new Window("Data status", 20, 0, Dictionary.GetDataStatus());
        Console.CursorVisible = false;
    }
    public void Run()
    {
        Console.Clear();
        _mainMenu.Draw();
        _dataStatus.Draw();
        SelectMainMenuItem();
    }

    private void SelectMainMenuItem()
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
                        Run();
                        break;
                    case Menues.Main.Outdoor_data:
                        break;
                    case Menues.Main.Indoor_data:
                        break;
                    case Menues.Main.Write_file:
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
    public List<string> GetMenuItems<TEnum>() where TEnum : Enum
    {
        var results = new List<string>();
        foreach (var menuItem in Enum.GetValues(typeof(TEnum)))
        {
            results.Add($"{(int)menuItem}. {menuItem.ToString()?.Replace('_', ' ')}");
        }
        return results;
    }
}
