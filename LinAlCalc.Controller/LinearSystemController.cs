using LinAlCalc.DataProcessing;
using LinAlCalc.Solver;
using MathNet.Numerics.LinearAlgebra;

namespace LinAlCalc.Controller
{
    public class LinearSystemController
    {
        // Метод для обработки входных данных и решения системы
        public SolutionResult SolveSystem(string input)
        {
            try
            {
                // Парсим входные данные
                var system = DataProcessor.ParseInput(input);

                // Валидируем входные данные
                DataProcessor.ValidateInput(system);

                // Преобразуем в формат MathNet.Numerics
                var A = Matrix<double>.Build.DenseOfArray(system.Coefficients);
                var b = Vector<double>.Build.DenseOfArray(system.Constants);

                // Решаем систему
                return LinearSystemSolver.Solve(A, b);
            }
            catch (ArgumentException ex)
            {
                return new SolutionResult
                {
                    Status = SolutionStatus.Unknown,
                    Solutions = new System.Collections.Generic.Dictionary<string, string>(),
                    ResidualNorm = double.NaN
                };
            }
            catch (Exception)
            {
                return new SolutionResult
                {
                    Status = SolutionStatus.Unknown,
                    Solutions = new System.Collections.Generic.Dictionary<string, string>(),
                    ResidualNorm = double.NaN
                };
            }
        }

        // Метод для проверки корректности входных данных
        public bool ValidateInput(string input, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                var system = DataProcessor.ParseInput(input);
                DataProcessor.ValidateInput(system);
                return true;
            }
            catch (ArgumentException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "Произошла неизвестная ошибка при обработке входных данных.";
                return false;
            }
        }
    }
}