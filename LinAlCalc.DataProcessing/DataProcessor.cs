using System.Text.RegularExpressions;
using System.Globalization;

namespace LinAlCalc.DataProcessing
{
    public class DataProcessor
    {
        public static LinearSystem ParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Входные данные не могут быть пустыми.");

            var lines = input.Trim().Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
                throw new ArgumentException("Не удалось разобрать уравнения.");

            var coefficients = new List<double[]>();
            var constants = new List<double>();

            foreach (var line in lines)
            {
                var (coeffs, constant) = ParseEquation(line);

                if (coefficients.Count != 0 && coeffs.Length != coefficients[0].Length)
                    throw new ArgumentException("Все уравнения должны содержать одинаковое количество переменных.");

                coefficients.Add(coeffs);
                constants.Add(constant);
            }

            int rowCount = coefficients.Count;
            int colCount = coefficients[0].Length;
            var A = new double[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                    A[i, j] = coefficients[i][j];

            return new LinearSystem
            {
                Coefficients = A,
                Constants = [.. constants]
            };
        }

        private static (double[] coefficients, double constant) ParseEquation(string equation)
        {
            equation = equation.Replace(" ", "").ToLower();

            if (equation.Contains("*x") || equation.Contains('^'))
                throw new ArgumentException($"Уравнение '{equation}' содержит нелинейные члены, которые не поддерживаются.");

            var parts = equation.Split('=');
            if (parts.Length != 2)
                throw new ArgumentException($"Уравнение '{equation}' не содержит знак '='.");

            string leftSide = parts[0];
            double constant = ParseNumber(parts[1]);

            var coefficients = new Dictionary<int, double>(); // Индекс переменной -> коэффициент
            var terms = SplitTerms(leftSide);

            foreach (var term in terms)
            {
                if (string.IsNullOrEmpty(term))
                    continue;

                double sign = term.StartsWith('-') ? -1 : 1;
                string termWithoutSign = term.StartsWith('+') || term.StartsWith('-') ? term[1..] : term;

                double coeff = 1.0;
                int varIndex = -1;

                var match = Regex.Match(termWithoutSign, @"^(\d+/\d+|[-]?\d*\.?\d*)(x(\d+))?$");
                if (!match.Success)
                    throw new ArgumentException($"Некорректный член уравнения: {term}");

                string coeffStr = match.Groups[1].Value;
                string varStr = match.Groups[3].Value;

                if (!string.IsNullOrEmpty(coeffStr))
                    coeff = ParseNumber(coeffStr);

                if (!string.IsNullOrEmpty(varStr))
                {
                    if (!int.TryParse(varStr, out varIndex))
                        throw new ArgumentException($"Некорректный индекс переменной в {term}");
                    varIndex--;
                }
                else
                {
                    constant -= sign * coeff;
                    continue;
                }

                coeff *= sign;

                if (coefficients.ContainsKey(varIndex))
                    coefficients[varIndex] += coeff;
                else
                    coefficients[varIndex] = coeff;
            }

            int maxIndex = coefficients.Keys.DefaultIfEmpty(-1).Max() + 1;
            if (maxIndex <= 0)
                throw new ArgumentException($"Уравнение не содержит переменных: {equation}");

            var result = new double[maxIndex];
            foreach (var kvp in coefficients)
            {
                if (kvp.Key >= 0)
                    result[kvp.Key] = kvp.Value;
            }

            return (result, constant);
        }
        
        public static IEnumerable<string> SplitTerms(string expression)
        {
            var terms = new List<string>();
            string currentTerm = "";
            bool firstTerm = true;

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if ((c == '+' || c == '-') && !firstTerm)
                {
                    if (!string.IsNullOrEmpty(currentTerm))
                        terms.Add(currentTerm);
                    currentTerm = c.ToString();
                }
                else
                {
                    currentTerm += c;
                }

                firstTerm = false;
            }

            if (!string.IsNullOrEmpty(currentTerm))
                terms.Add(currentTerm);

            return terms;
        }

        private static double ParseNumber(string numberStr)
        {
            numberStr = numberStr.Trim();
            if (string.IsNullOrEmpty(numberStr))
                throw new ArgumentException("Число не указано.");

            try
            {
                if (numberStr.Contains('/'))
                {
                    var parts = numberStr.Split('/');
                    if (parts.Length != 2)
                        throw new ArgumentException($"Некорректный формат дроби: {numberStr}");

                    double numerator = double.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);
                    double denominator = double.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);

                    if (Math.Abs(denominator) < 1e-10)
                        throw new ArgumentException("Деление на ноль.");

                    return numerator / denominator;
                }

                return double.Parse(numberStr, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new ArgumentException($"Некорректный формат числа: {numberStr}");
            }
        }

        public static void ValidateInput(LinearSystem system)
        {
            if (system == null || system.Coefficients == null || system.Constants == null)
                throw new ArgumentException("Система или её компоненты не могут быть null.");

            int rowCount = system.RowCount;
            int colCount = system.ColumnCount;

            if (rowCount == 0 || colCount == 0)
                throw new ArgumentException("Матрица не может быть пустой.");

            if (system.Constants.Length != rowCount)
                throw new ArgumentException("Размер вектора констант не соответствует количеству строк матрицы.");

            for (int i = 0; i < rowCount; i++)
            {
                bool allZero = true;
                for (int j = 0; j < colCount; j++)
                {
                    if (Math.Abs(system.Coefficients[i, j]) > 1e-10)
                    {
                        allZero = false;
                        break;
                    }
                }

                if (allZero && Math.Abs(system.Constants[i]) > 1e-10)
                    throw new ArgumentException("Система содержит противоречие: строка с нулевыми коэффициентами и ненулевым свободным членом.");
            }
        }
    }
}