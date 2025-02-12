namespace Arch_VaderData;

public class Menues
{
    public enum Main
    {
        Exit = 0,
        Read_data,
        Outdoor_data,
        Indoor_data,
        Write_file,
    }
    public enum Outdoor
    {
        Back = 0,
        Search_date,
        Show_warmest_to_coldest,
        Show_driest_to_most_humid,
        Show_mold_risk,
        Meterological_autumn,
        Meterological_winter,
        Show_pleasant_days,
    }
    public enum Indoor
    {
        Back = 0,
        Search_date,
        Show_warmest_to_coldest,
        Show_driest_to_most_humid,
        Show_mold_risk,
        Show_open_balcony_door_times,
    }
}