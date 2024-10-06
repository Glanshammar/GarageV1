namespace Garage_V1.FileHandling;

public class JsonHandler : FileReader
{
    public JsonHandler(string filePath) : base(filePath)
    {
        if (!filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("File must have a .json extension", nameof(filePath));
        }
    }

    public override string ReadContent()
    {
        return File.ReadAllText(FilePath);
    }

    public JObject? ReadAsJObject()
    {
        string jsonContent = ReadContent();
        try
        {
            return JObject.Parse(jsonContent);
        }
        catch (JsonReaderException)
        {
            return null;
        }
    }

    public T? ReadAs<T>() where T : class
    {
        string jsonContent = ReadContent();
        return JsonConvert.DeserializeObject<T>(jsonContent);
    }

    public void WriteContent(object? content)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));
        
        string jsonContent = JsonConvert.SerializeObject(content, Formatting.Indented);
        File.WriteAllText(FilePath, jsonContent);
    }

    public void UpdateProperty(string propertyPath, JToken newValue)
    {
        JObject? jsonObject = ReadAsJObject();
        if (jsonObject == null)
        {
            throw new InvalidOperationException("Failed to read JSON content as JObject.");
        }

        JToken? token = jsonObject.SelectToken(propertyPath);
        if (token != null)
        {
            token.Replace(newValue);
            WriteContent(jsonObject);
        }
        else
        {
            throw new ArgumentException($"Property path '{propertyPath}' not found in the JSON file.");
        }
    }

    protected override bool ValidateFile()
    {
        if (!base.ValidateFile()) return false;

        try
        {
            JObject.Parse(ReadContent());
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }
}