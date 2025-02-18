using Arch_VaderData.Helpers;

namespace Arch_VaderData;

internal class Program
{
    static void Main()
    {
        GetData.ReadAllData();
        CreateFile.FileCreator();
        UserInterface.StartMenu();
        //GetData.ReadAllData();
        //DataInOrder.AvgAMonth("Inside");
    }
}

