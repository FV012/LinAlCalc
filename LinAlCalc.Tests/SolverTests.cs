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
        public void Solve_UniqueSolution_ReturnsCorrectResult()
        {
            // Arrange
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1 }, { 1, -1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 5, 1 });

            // Act
            var result = LinearSystemSolver.Solve(A, b);

            // Assert
            Assert.AreEqual(SolutionStatus.UniqueSolution, result.Status);
            Assert.AreEqual(2, result.Solutions.Count);
            Assert.IsTrue(result.Solutions.ContainsKey("x1"));
            Assert.IsTrue(result.Solutions.ContainsKey("x2"));
            Assert.AreEqual(2.0, double.Parse(result.Solutions["x1"], System.Globalization.CultureInfo.InvariantCulture), 1e-10);
            Assert.AreEqual(1.0, double.Parse(result.Solutions["x2"], System.Globalization.CultureInfo.InvariantCulture), 1e-10);
            Assert.IsTrue(result.ResidualNorm < 1e-10);
        }

        [TestMethod]
        public void Solve_NoSolution_ReturnsNoSolutionStatus()
        {
            // Arrange
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 }, { 1, 1 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 2, 3 });

            // Act
            var result = LinearSystemSolver.Solve(A, b);

            // Assert
            Assert.AreEqual(SolutionStatus.NoSolution, result.Status);
            Assert.AreEqual(0, result.Solutions.Count);
        }

        [TestMethod]
        public void Solve_InfiniteSolutions_ReturnsInfiniteSolutionsStatus()
        {
            // Arrange
            var A = Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 1 }, { 2, 2 } });
            var b = Vector<double>.Build.DenseOfArray(new double[] { 2, 4 });

            // Act
            var result = LinearSystemSolver.Solve(A, b);

            // Assert
            Assert.AreEqual(SolutionStatus.InfiniteSolutions, result.Status);
            Assert.AreEqual(0, result.Solutions.Count);
        }

        [TestMethod]
        public void ToSymbolicFraction_IntegerValue_ReturnsIntegerString()
        {
            // Arrange
            double value = 3.0;

            // Act
            var result = LinearSystemSolver.ToSymbolicFraction(value);

            // Assert
            Assert.AreEqual("3", result);
        }

        [TestMethod]
        public void ToSymbolicFraction_FractionalValue_ReturnsFraction()
        {
            // Arrange
            double value = 0.5;

            // Act
            var result = LinearSystemSolver.ToSymbolicFraction(value);

            // Assert
            Assert.AreEqual("1/2", result);
        }
    }
}