using System;
namespace Arch_VaderData;

public class FileReader
{
    private string _filename;
    private DataParser _dataParser;

    public FileReader(string filename, DataParser dataParser)
    {
        _filename = filename;
        _dataParser = dataParser;
    }

    public void ParseFile()
    {
        var fileContent = File.ReadLines(_filename);
        int index = 0;
        foreach (var line in fileContent)
        {
            try
            {
                _dataParser.AddDataLine(line, index);
                index++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
