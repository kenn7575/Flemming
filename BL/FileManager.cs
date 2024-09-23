
namespace BL
{

public class FileManager
{
    //function to read the file
    public static string ReadFile(string path)
    {
        string text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), path));
        return text;
    }
}


}
