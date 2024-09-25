
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

        public static void SaveJson(string path, string json)
        {

            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), path), json);

        }
    }

     
}
