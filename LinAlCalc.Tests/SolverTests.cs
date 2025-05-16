using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinAlCalc.DataProcessing;
using LinAlCalc.Solver;
using MathNet.Numerics.LinearAlgebra;
using System;

namespace LinAlCalc.Tests
{
    [TestClass]
    public class SolverTests
    {
        [TestMethod]
        public void Solve_UniqueSolution_ReturnsUniqueStatus()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1 }, { 1, -1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 5, 1 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(SolutionStatus.UniqueSolution, result.Status);
        }

        [TestMethod]
        public void Solve_UniqueSolution_HasTwoSolutions()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1 }, { 1, -1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 5, 1 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(2, result.Solutions.Count);
        }

        [TestMethod]
        public void Solve_UniqueSolution_ContainsX1()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1 }, { 1, -1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 5, 1 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.IsTrue(result.Solutions.ContainsKey("x1"));
        }

        [TestMethod]
        public void Solve_UniqueSolution_ContainsX2()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1 }, { 1, -1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 5, 1 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.IsTrue(result.Solutions.ContainsKey("x2"));
        }

        [TestMethod]
        public void Solve_UniqueSolution_X1IsCorrect()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1 }, { 1, -1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 5, 1 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(2.0, double.Parse(result.Solutions["x1"], System.Globalization.CultureInfo.InvariantCulture), 1e-10);
        }

        [TestMethod]
        public void Solve_UniqueSolution_X2IsCorrect()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1 }, { 1, -1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 5, 1 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(1.0, double.Parse(result.Solutions["x2"], System.Globalization.CultureInfo.InvariantCulture), 1e-10);
        }

        [TestMethod]
        public void Solve_UniqueSolution_ResidualNormIsSmall()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1 }, { 1, -1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 5, 1 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.IsTrue(result.ResidualNorm < 1e-10);
        }

        [TestMethod]
        public void Solve_NoSolution_ReturnsNoSolutionStatus()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 }, { 1, 1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 2, 3 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(SolutionStatus.NoSolution, result.Status);
        }

        [TestMethod]
        public void Solve_NoSolution_HasEmptySolutions()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 }, { 1, 1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 2, 3 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(0, result.Solutions.Count);
        }

        [TestMethod]
        public void Solve_InfiniteSolutions_ReturnsInfiniteSolutionsStatus()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 }, { 2, 2 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 2, 4 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(SolutionStatus.InfiniteSolutions, result.Status);
        }

        [TestMethod]
        public void Solve_InfiniteSolutions_HasEmptySolutions()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 }, { 2, 2 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 2, 4 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(0, result.Solutions.Count);
        }

        [TestMethod]
        public void Solve_SingularMatrix_ReturnsInfiniteSolutions()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 }, { 1, 1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 2, 2 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(SolutionStatus.InfiniteSolutions, result.Status);
        }

        [TestMethod]
        public void Solve_SingularMatrix_HasEmptySolutions()
        {
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 }, { 1, 1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 2, 2 });
            var result = LinearSystemSolver.Solve(A, b);
            Assert.AreEqual(0, result.Solutions.Count);
        }

        [TestMethod]
        public void ToSymbolicFraction_IntegerValue_ReturnsIntegerString()
        {
            double value = 3.0;
            var result = LinearSystemSolver.ToSymbolicFraction(value);
            Assert.AreEqual("3", result);
        }

        [TestMethod]
        public void ToSymbolicFraction_FractionalValue_ReturnsFraction()
        {
            double value = 0.5;
            var result = LinearSystemSolver.ToSymbolicFraction(value);
            Assert.AreEqual("1/2", result);
        }

        [TestMethod]
        public void ToSymbolicFraction_NegativeFraction_ReturnsCorrectFraction()
        {
            double value = -0.5;
            var result = LinearSystemSolver.ToSymbolicFraction(value);
            Assert.AreEqual("-1/2", result);
        }

        [TestMethod]
        public void ToSymbolicFraction_NearInteger_ReturnsInteger()
        {
            double value = 2.999999;
            var result = LinearSystemSolver.ToSymbolicFraction(value);
            Assert.AreEqual("3", result);
        }
    }
}