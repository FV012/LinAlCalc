namespace LinAlCalc.DataProcessing
{
    public class LinearSystem
    {
        public required double[,] Coefficients { get; set; }
        public required double[] Constants { get; set; }
        public int RowCount => Coefficients.GetLength(0);
        public int ColumnCount => Coefficients.GetLength(1);
    }
}
