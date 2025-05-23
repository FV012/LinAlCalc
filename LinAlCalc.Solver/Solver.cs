using MathNet.Numerics.LinearAlgebra;

namespace LinAlCalc.Solver
{
    public static class LinearSystemSolver
    {
        public static SolutionResult Solve(Matrix<double> A, Vector<double> b)
        {
            var result = new SolutionResult();
            int n = A.RowCount;
            int m = A.ColumnCount;

            if (b.Count != n)
                throw new ArgumentException("Размерность вектора b не совпадает с количеством строк матрицы A");

            var augmented = A.Append(b.ToColumnMatrix());
            int rankA = A.Rank();
            int rankAugmented = augmented.Rank();

            if (rankA != rankAugmented)
            {
                result.Status = SolutionStatus.NoSolution;
                return result;
            }

            if (rankA < m)
            {
                result.Status = SolutionStatus.InfiniteSolutions;
                return result;
            }

            Vector<double>? x = null;

            try
            {
                var lu = A.LU();
                if (Math.Abs(lu.Determinant) > 1e-12)
                {
                    x = lu.Solve(b);
                    result.Status = SolutionStatus.UniqueSolution;
                }
            }
            catch { }

            if (x == null)
            {
                try
                {
                    var qr = A.QR();
                    x = qr.Solve(b);
                    result.Status = SolutionStatus.UniqueSolution;
                }
                catch
                {
                    result.Status = SolutionStatus.Unknown;
                    return result;
                }
            }

            result.Solutions = [];
            for (int i = 0; i < x.Count; i++)
            {
                string varName = $"x{i + 1}";
                string sym = ToSymbolicFraction(x[i]);
                result.Solutions[varName] = sym;
            }

            result.ResidualNorm = (A * x - b).L2Norm();

            return result;
        }

        public static string ToSymbolicFraction(double value, double tolerance = 1e-5)
        {
            double roundedValue = Math.Round(value);
            if (Math.Abs(value - roundedValue) < tolerance)
                return ((int)roundedValue).ToString();

            int maxDen = 1000;
            int bestNum = 0;
            int bestDen = 1;
            double bestError = double.MaxValue;

            for (int den = 1; den <= maxDen; den++)
            {
                int num = (int)Math.Round(value * den);
                double error = Math.Abs(value - (double)num / den);
                if (error < bestError)
                {
                    bestError = error;
                    bestNum = num;
                    bestDen = den;
                }

                if (bestError < tolerance)
                    break;
            }

            return $"{bestNum}/{bestDen}";
        }
    }
}