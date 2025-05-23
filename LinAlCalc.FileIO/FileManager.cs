using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LinAlCalc.FileIO
{
    public class FileManager
    {
        // Сохраняет входные данные в файл .csv
        public async Task SaveInputAsync(string input, string filePath)
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

        // Читает входные данные из файла .csv и преобразует в формат уравнений
        public async Task<string> ReadInputAsync(string filePath)
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

                    // Формируем уравнение
                    var equation = new StringBuilder();
                    bool firstTerm = true;
                    for (int i = 0; i < values.Length - 1; i++)
                    {
                        double coeff = values[i];
                        if (Math.Abs(coeff) < 1e-10) // Пропускаем нулевые коэффициенты
                            continue;

                        if (!firstTerm && coeff > 0)
                            equation.Append("+");

                        if (Math.Abs(coeff) == 1.0)
                            equation.Append(coeff < 0 ? "-" : "");
                        else if (Math.Abs(coeff) == -1.0)
                            equation.Append("-");
                        else
                            equation.Append(coeff.ToString(CultureInfo.InvariantCulture));

                        equation.Append($"x{i + 1}");
                        firstTerm = false;
                    }

                    // Добавляем свободный член
                    double constant = values[values.Length - 1];
                    equation.Append($" = {constant.ToString(CultureInfo.InvariantCulture)}");

                    // Если все коэффициенты нулевые, но есть константа
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

        // Читает матрицу из файла .csv для матричного ввода
        public async Task<(double[,] coefficients, double[] constants)> ReadMatrixInputAsync(string filePath)
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

                // Парсим строки
                var coefficients = new System.Collections.Generic.List<double[]>();
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
                int colCount = coefficients[0].Length - 1; // Последний столбец - константа
                if (coefficients.Any(row => row.Length != colCount + 1))
                    throw new ArgumentException("Все строки .csv должны иметь одинаковое количество столбцов.");

                // Создаём матрицу коэффициентов и вектор констант
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