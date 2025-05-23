namespace LinAlCalc.Solver
{
    public class SolutionResult
    {
        public Dictionary<string, string> Solutions { get; set; } = [];
        public SolutionStatus Status { get; set; } = SolutionStatus.Unknown;
        public double ResidualNorm { get; set; } = double.NaN;

        public override string ToString()
        {
            if (Status == SolutionStatus.NoSolution)
                return "Система не имеет решений.";
            if (Status == SolutionStatus.InfiniteSolutions)
                return "Система имеет бесконечно много решений.";
            if (Status == SolutionStatus.UniqueSolution)
            {
                string result = "Решение:\n";
                foreach (var pair in Solutions)
                    result += $"{pair.Key} = {pair.Value}\n";
                result += $"Погрешность (норма невязки): {ResidualNorm}";
                return result;
            }

            return "Статус решения не определён.";
        }
    }
}