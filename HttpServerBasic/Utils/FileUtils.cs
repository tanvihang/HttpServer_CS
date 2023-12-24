namespace HttpServerBasic;

public class FileUtils
{
    private static readonly object FileLock = new object();

    public static void WriteToFile(string filePath, string text)
    {
        lock (FileLock)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(text);
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine($"Error writeing to the file: {filePath}");
            }
        }
    }
    
    //Input /Resources/Static/css/main.css
    public static string GetFilePath(string pathFromRoot)
    {
        string baseDirectory = AppContext.BaseDirectory;
        string indexDirectory = Path.Combine(baseDirectory, "..", "..", "..");

        string[] split = pathFromRoot.Split("/");

        foreach (string item in split)
        {
            indexDirectory = Path.Combine(indexDirectory, item);
        }
        
        indexDirectory = Path.GetFullPath(indexDirectory);
        
        return indexDirectory;
    }
}