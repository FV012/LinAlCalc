using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinAlCalc.DataProcessing;
using LinAlCalc.Solver;
using MathNet.Numerics.LinearAlgebra;
using System;

namespace LinAlCalc.Tests
{
    [TestClass]
    public class DataProcessorTests
    {
        [TestMethod]
        public void ParseInput_ValidInput_ReturnsCorrectLinearSystem()
        {
            // Arrange
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";

            // Act
            var system = DataProcessor.ParseInput(input);

            // Assert
            Assert.AreEqual(2, system.RowCount);
            Assert.AreEqual(2, system.ColumnCount);
            Assert.AreEqual(2.0, system.Coefficients[0, 0], 1e-10);
            Assert.AreEqual(1.0, system.Coefficients[0, 1], 1e-10);
            Assert.AreEqual(1.0, system.Coefficients[1, 0], 1e-10);
            Assert.AreEqual(-1.0, system.Coefficients[1, 1], 1e-10);
            Assert.AreEqual(5.0, system.Constants[0], 1e-10);
            Assert.AreEqual(1.0, system.Constants[1], 1e-10);
        }

        [TestMethod]
        public void ParseInput_FractionalCoefficients_ParsesCorrectly()
        {
            // Arrange
            string input = "1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";

            // Act
            var system = DataProcessor.ParseInput(input);

            // Assert
            Assert.AreEqual(0.5, system.Coefficients[0, 0], 1e-10);
            Assert.AreEqual(0.75, system.Coefficients[0, 1], 1e-10);
            Assert.AreEqual(1.0, system.Coefficients[1, 0], 1e-10);
            Assert.AreEqual(-1.0, system.Coefficients[1, 1], 1e-10);
            Assert.AreEqual(5.0, system.Constants[0], 1e-10);
            Assert.AreEqual(1.0, system.Constants[1], 1e-10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_EmptyInput_ThrowsArgumentException()
        {
            // Arrange
            string input = "";

            // Act
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_NonLinearEquation_ThrowsArgumentException()
        {
            // Arrange
            string input = "x1^2 + x2 = 5";

            // Act
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_InconsistentVariableCount_ThrowsArgumentException()
        {
            // Arrange
            string input = "x1 + x2 = 5\nx1 = 1";

            // Act
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        public void ValidateInput_ValidSystem_DoesNotThrow()
        {
            // Arrange
            var system = new DataProcessor.LinearSystem
            {
                Coefficients = new double[,] { { 2, 1 }, { 1, -1 } },
                Constants = new double[] { 5, 1 }
            };

            // Act & Assert
            DataProcessor.ValidateInput(system);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateInput_ZeroRowWithNonZeroConstant_ThrowsArgumentException()
        {
            // Arrange
            var system = new DataProcessor.LinearSystem
            {
                Coefficients = new double[,] { { 0, 0 }, { 1, -1 } },
                Constants = new double[] { 5, 1 }
            };

            // Act
            DataProcessor.ValidateInput(system);
        }
    }
}