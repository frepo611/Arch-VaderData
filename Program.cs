namespace Arch_VaderData;

internal class Program
{
    private static void Main()
    {


        GetData.ReadAllData();

        //var yes = Models.Dictionary.Data;


        //Models.Inside inside = new Models.Inside(5);
        //Models.Outside outside = new Models.Outside(5,5);
        DateTime dateTime = new DateTime (2016,07,29);



        //Console.WriteLine(dateTime.ToString("yyyy-MM-dd"));

        //Models.Dictionary dictionary = new Models.Dictionary();
        //dictionary.Data.Add(dateTime, (inside, outside));


  


        var test = Models.Dictionary.Data[dateTime];


        Models.Inside inside1 = test.Item1;
        Models.Outside outside1 = test.Item2;



        Console.ReadLine(); 







        //AverageSetDay.AverageTempAndHumidityOutside("Inne");

        //Console.ReadLine();

        //AverageSetDay.AverageTempAndHumidityOutside("Ute");

        //Console.ReadLine();
    }
}

