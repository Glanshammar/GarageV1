namespace Garage_V1.FileHandling;

public abstract class FileReader
{
    protected string FilePath { get; set; }

    protected FileReader(string filePath)
    {
        FilePath = filePath;
    }

    public abstract string ReadContent();

    protected virtual bool ValidateFile()
    {
        return File.Exists(FilePath);
    }

    public virtual void DisplayFileInfo()
    {
        Console.WriteLine($"File path: {FilePath}");
    }
}