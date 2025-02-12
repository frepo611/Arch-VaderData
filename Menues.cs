namespace Arch_VaderData;

public class Menues
{
    public enum Main
    {
        Exit = 0,
        Read_data,
        Outdoor_data,
        Indoor_data,
        Write_file, // Att göra
    }
    public enum Outdoor
    {
        Back = 0,
        Search_date,
        Show_warmest_to_coldest,
        Show_driest_to_most_humid,
        Show_mold_risk, // Att göra
        Meterological_autumn, // Att göra
        Meterological_winter, // Att göra
        Show_pleasant_days, // Att göra??
    }
    public enum Indoor
    {
        Back = 0,
        Search_date,
        Show_warmest_to_coldest,
        Show_driest_to_most_humid,
        Show_mold_risk, // Att göra
        Show_open_balcony_door_times, // Att göra??
    }
}