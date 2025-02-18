namespace Arch_VaderData;

public class Menues
{
    public enum Main
    {
        Exit = 0,
        Read_data,
        Outside_data,
        Inside_data,
        Write_file, // Att göra
    }
    public enum Outside
    {
        Back = 0,
        Show_measurement_for_date,
        Show_warmest_to_coldest,
        Show_driest_to_most_humid,
        Show_mold_risks_lowest_to_highests,
        Meterological_autumn,
        Meterological_winter,
    }
    public enum Inside
    {
        Back = 0,
        Show_measurement_for_date,
        Show_warmest_to_coldest,
        Show_driest_to_most_humid,
        Show_mold_risks_lowest_to_highests,
    }
}