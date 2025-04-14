using System.Text;

namespace LinAlCalc.FileIO
{
    public class FileManager
    {
        // Сохраняет входные данные в файл
        public async Task SaveInputAsync(string input, string filePath)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Входные данные не могут быть пустыми.");

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не указан.");

            try
            {
                await File.WriteAllTextAsync(filePath, input, Encoding.UTF8);
            }
            catch (UnauthorizedAccessException)
            {
                throw new IOException($"Нет доступа для записи в файл: {filePath}");
            }
            catch (DirectoryNotFoundException)
            {
                throw new IOException($"Указанная директория не существует: {Path.GetDirectoryName(filePath)}");
            }
            catch (Exception ex)
            {
                throw new IOException($"Ошибка при сохранении файла: {ex.Message}");
            }
        }

        // Читает входные данные из файла
        public async Task<string> ReadInputAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не указан.");

            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл не найден: {filePath}");

                return await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            }
            catch (UnauthorizedAccessException)
            {
                throw new IOException($"Нет доступа для чтения файла: {filePath}");
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IOException($"Ошибка при чтении файла: {ex.Message}");
            }
        }

        // Сохраняет результат решения в файл
        public async Task SaveResultAsync(string result, string filePath)
        {
            if (string.IsNullOrWhiteSpace(result))
                throw new ArgumentException("Результат не может быть пустым.");

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не указан.");

            try
            {
                await File.WriteAllTextAsync(filePath, result, Encoding.UTF8);
            }
            catch (UnauthorizedAccessException)
            {
                throw new IOException($"Нет доступа для записи в файл: {filePath}");
            }
            catch (DirectoryNotFoundException)
            {
                throw new IOException($"Указанная директория не существует: {Path.GetDirectoryName(filePath)}");
            }
            catch (Exception ex)
            {
                throw new IOException($"Ошибка при сохранении файла: {ex.Message}");
            }
        }

        // Проверяет, существует ли файл
        public bool FileExists(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            return File.Exists(filePath);
        }

        // Проверяет, доступна ли директория для записи
        public bool IsDirectoryWritable(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return false;

            try
            {
                Directory.CreateDirectory(directoryPath); // Создаёт, если не существует
                string tempFile = Path.Combine(directoryPath, Guid.NewGuid().ToString() + ".tmp");
                File.WriteAllText(tempFile, "test");
                File.Delete(tempFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}