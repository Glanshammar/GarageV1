namespace Garage_V1.FileHandling;

public static class JsonHandler
{
    public static void Create(string filePath)
    {
        ValidateFilePath(filePath);

        if (File.Exists(filePath))
            throw new IOException($"File already exists: {filePath}");

        // Create an empty JSON object
        JObject emptyObject = new JObject();
        string jsonContent = emptyObject.ToString(Formatting.Indented);
        File.WriteAllText(filePath, jsonContent);
    }

    public static string ReadContent(string filePath)
    {
        ValidateFilePath(filePath);
        return File.ReadAllText(filePath);
    }

    public static JObject? ReadAsJObject(string filePath)
    {
        ValidateFilePath(filePath);
        string jsonContent = ReadContent(filePath);
        try
        {
            return JObject.Parse(jsonContent);
        }
        catch (JsonReaderException)
        {
            return null;
        }
    }

    public static T? ReadAs<T>(string filePath) where T : class
    {
        ValidateFilePath(filePath);
        string jsonContent = ReadContent(filePath);
        return JsonConvert.DeserializeObject<T>(jsonContent);
    }

    public static void WriteContent(string filePath, object? content)
    {
        ValidateFilePath(filePath);
        if (content == null)
            throw new ArgumentNullException(nameof(content));
        
        string jsonContent = JsonConvert.SerializeObject(content, Formatting.Indented);
        File.WriteAllText(filePath, jsonContent);
    }

    public static void UpdateProperty(string filePath, string propertyPath, JToken newValue)
    {
        ValidateFilePath(filePath);
        JObject? jsonObject = ReadAsJObject(filePath);
        if (jsonObject == null)
        {
            throw new InvalidOperationException("Failed to read JSON content as JObject.");
        }

        JToken? token = jsonObject.SelectToken(propertyPath);
        if (token != null)
        {
            token.Replace(newValue);
            WriteContent(filePath, jsonObject);
        }
        else
        {
            throw new ArgumentException($"Property path '{propertyPath}' not found in the JSON file.");
        }
    }

    public static bool ValidateFile(string filePath)
    {
        ValidateFilePath(filePath);
        if (!File.Exists(filePath)) return false;

        try
        {
            JObject.Parse(ReadContent(filePath));
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }

    private static void ValidateFilePath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (!filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("File must have a .json extension", nameof(filePath));
    }
}