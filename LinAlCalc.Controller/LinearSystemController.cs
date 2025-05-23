using LinAlCalc.DataProcessing;
using LinAlCalc.Solver;
using MathNet.Numerics.LinearAlgebra;

namespace LinAlCalc.Controller
{
    public class LinearSystemController
    {
        public static SolutionResult SolveSystem(string input)
        {
            try
            {
                var system = DataProcessor.ParseInput(input);
                DataProcessor.ValidateInput(system);

                var A = Matrix<double>.Build.DenseOfArray(system.Coefficients);
                var b = Vector<double>.Build.DenseOfArray(system.Constants);

                return LinearSystemSolver.Solve(A, b);
            }
            catch (ArgumentException)
            {
                return new SolutionResult
                {
                    Status = SolutionStatus.Unknown,
                    Solutions = [],
                    ResidualNorm = double.NaN
                };
            }
            catch (Exception)
            {
                return new SolutionResult
                {
                    Status = SolutionStatus.Unknown,
                    Solutions = [],
                    ResidualNorm = double.NaN
                };
            }
        }

        public static bool ValidateInput(string input, out string errorMessage)
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
            catch (Exception)
            {
                errorMessage = "Произошла неизвестная ошибка при обработке входных данных.";
                return false;
            }
        }
    }
}