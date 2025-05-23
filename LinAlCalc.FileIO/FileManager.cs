using System.Text;
using System.Globalization;

namespace LinAlCalc.FileIO
{
    public class FileManager
    {
        public static async Task SaveInputAsync(string input, string filePath)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Входные данные не могут быть пустыми.");

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не указан.");

            if (!filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Файл должен иметь расширение .csv");

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

        public static async Task<string> ReadInputAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не указан.");

            if (!filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Файл должен иметь расширение .csv");

            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл не найден: {filePath}");

                string[] lines = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
                if (lines.Length == 0)
                    throw new ArgumentException("Файл .csv пуст.");

                var equations = new StringBuilder();
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var values = line.Split(',')
                        .Select(v => double.Parse(v.Trim(), CultureInfo.InvariantCulture))
                        .ToArray();

                    if (values.Length < 2)
                        throw new ArgumentException("Каждая строка .csv должна содержать хотя бы один коэффициент и константу.");

                    var equation = new StringBuilder();
                    bool firstTerm = true;
                    for (int i = 0; i < values.Length - 1; i++)
                    {
                        double coeff = values[i];
                        if (Math.Abs(coeff) < 1e-10)
                            continue;

                        if (!firstTerm && coeff > 0)
                            equation.Append('+');

                        if (Math.Abs(coeff) == 1.0)
                            equation.Append(coeff < 0 ? "-" : "");
                        else if (Math.Abs(coeff) == -1.0)
                            equation.Append('-');
                        else
                            equation.Append(coeff.ToString(CultureInfo.InvariantCulture));

                        equation.Append($"x{i + 1}");
                        firstTerm = false;
                    }

                    double constant = values[^1];
                    equation.Append($" = {constant.ToString(CultureInfo.InvariantCulture)}");

                    if (firstTerm)
                        equation.Insert(0, "0");

                    equations.AppendLine(equation.ToString());
                }

                return equations.ToString().TrimEnd();
            }
            catch (UnauthorizedAccessException)
            {
                throw new IOException($"Нет доступа для чтения файла: {filePath}");
            }
            catch (FormatException)
            {
                throw new ArgumentException("Некорректный формат чисел в файле .csv.");
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

        public static async Task<(double[,] coefficients, double[] constants)> ReadMatrixInputAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не указан.");

            if (!filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Файл должен иметь расширение .csv");

            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл не найден: {filePath}");

                string[] lines = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
                if (lines.Length == 0)
                    throw new ArgumentException("Файл .csv пуст.");

                var coefficients = new List<double[]>();
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var values = line.Split(',')
                        .Select(v => double.Parse(v.Trim(), CultureInfo.InvariantCulture))
                        .ToArray();

                    if (values.Length < 2)
                        throw new ArgumentException("Каждая строка .csv должна содержать хотя бы один коэффициент и константу.");

                    coefficients.Add(values);
                }

                if (coefficients.Count == 0)
                    throw new ArgumentException("Файл .csv не содержит валидных данных.");

                int rowCount = coefficients.Count;
                int colCount = coefficients[0].Length - 1; 
                if (coefficients.Any(row => row.Length != colCount + 1))
                    throw new ArgumentException("Все строки .csv должны иметь одинаковое количество столбцов.");

                var A = new double[rowCount, colCount];
                var b = new double[rowCount];

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        A[i, j] = coefficients[i][j];
                    }
                    b[i] = coefficients[i][colCount];
                }

                return (A, b);
            }
            catch (UnauthorizedAccessException)
            {
                throw new IOException($"Нет доступа для чтения файла: {filePath}");
            }
            catch (FormatException)
            {
                throw new ArgumentException("Некорректный формат чисел в файле .csv.");
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

        public static async Task SaveResultAsync(string result, string filePath)
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

        public static bool FileExists(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            return File.Exists(filePath);
        }

        public static bool IsDirectoryWritable(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return false;

            try
            {
                Directory.CreateDirectory(directoryPath); 
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