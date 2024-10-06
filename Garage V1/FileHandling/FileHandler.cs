namespace Garage_V1.FileHandling;

public static class FileHandler
    {
        private static string CurrentFilePath { get; set; }

        public static void SetFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
            CurrentFilePath = filePath;
        }

        public static void Create(string filePath = null)
        {
            string path = filePath ?? CurrentFilePath;
            if (string.IsNullOrEmpty(path))
            {
                throw new InvalidOperationException("File path is not set.");
            }

            using (FileStream fs = File.Create(path))
            {
                Console.WriteLine($"File created: {path}.");
            }
        }

        public static void Delete(string filePath = null)
        {
            string path = filePath ?? CurrentFilePath;
            if (string.IsNullOrEmpty(path))
            {
                throw new InvalidOperationException("File path is not set.");
            }

            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine($"File deleted: {path}.");
            }
            else
            {
                Console.WriteLine($"File not found: {path}.");
            }
        }

        public static void Copy(string destinationPath, string sourceFilePath = null)
        {
            string sourcePath = sourceFilePath ?? CurrentFilePath;
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new InvalidOperationException("Source file path is not set.");
            }

            File.Copy(sourcePath, destinationPath, true);
            Console.WriteLine($"File copied from {sourcePath} to {destinationPath}.");
        }

        public static void Move(string destinationPath, string sourceFilePath = null)
        {
            string sourcePath = sourceFilePath ?? CurrentFilePath;
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new InvalidOperationException("Source file path is not set.");
            }

            File.Move(sourcePath, destinationPath);
            CurrentFilePath = destinationPath;
            Console.WriteLine($"File moved from {sourcePath} to {destinationPath}.");
        }
        
        public static bool ValidateFile(string filePath)
        {
            return File.Exists(filePath);
        }
    }