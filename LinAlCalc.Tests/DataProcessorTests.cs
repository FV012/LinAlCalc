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
        public static class DataProcessorTestsExtensions
        {
            public static List<string> SplitTerms(string expression)
            {
                return DataProcessor.SplitTerms(expression).ToList();
            }
        }

        [TestMethod]
        public void ParseInput_ValidInput_HasTwoRows()
        {
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(2, system.RowCount);
        }

        [TestMethod]
        public void ParseInput_ValidInput_HasTwoColumns()
        {
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(2, system.ColumnCount);
        }

        [TestMethod]
        public void ParseInput_ValidInput_FirstRowFirstCoefficient()
        {
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(2.0, system.Coefficients[0, 0], 1e-10);
        }

        [TestMethod]
        public void ParseInput_ValidInput_FirstRowSecondCoefficient()
        {
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(1.0, system.Coefficients[0, 1], 1e-10);
        }

        [TestMethod]
        public void ParseInput_ValidInput_SecondRowFirstCoefficient()
        {
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(1.0, system.Coefficients[1, 0], 1e-10);
        }

        [TestMethod]
        public void ParseInput_ValidInput_SecondRowSecondCoefficient()
        {
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(-1.0, system.Coefficients[1, 1], 1e-10);
        }

        [TestMethod]
        public void ParseInput_ValidInput_FirstConstant()
        {
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(5.0, system.Constants[0], 1e-10);
        }

        [TestMethod]
        public void ParseInput_ValidInput_SecondConstant()
        {
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(1.0, system.Constants[1], 1e-10);
        }

        [TestMethod]
        public void ParseInput_FractionalCoefficients_FirstRowFirstCoefficient()
        {
            string input = "1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(0.5, system.Coefficients[0, 0], 1e-10);
        }

        [TestMethod]
        public void ParseInput_FractionalCoefficients_FirstRowSecondCoefficient()
        {
            string input = "1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(0.75, system.Coefficients[0, 1], 1e-10);
        }

        [TestMethod]
        public void ParseInput_FractionalCoefficients_SecondRowFirstCoefficient()
        {
            string input = "1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(1.0, system.Coefficients[1, 0], 1e-10);
        }

        [TestMethod]
        public void ParseInput_FractionalCoefficients_SecondRowSecondCoefficient()
        {
            string input = "1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(-1.0, system.Coefficients[1, 1], 1e-10);
        }

        [TestMethod]
        public void ParseInput_FractionalCoefficients_FirstConstant()
        {
            string input = "1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(5.0, system.Constants[0], 1e-10);
        }

        [TestMethod]
        public void ParseInput_FractionalCoefficients_SecondConstant()
        {
            string input = "1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(1.0, system.Constants[1], 1e-10);
        }

        [TestMethod]
        public void ParseInput_NegativeFractionalCoefficients_FirstRowFirstCoefficient()
        {
            string input = "-1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(-0.5, system.Coefficients[0, 0], 1e-10);
        }

        [TestMethod]
        public void ParseInput_NegativeFractionalCoefficients_FirstRowSecondCoefficient()
        {
            string input = "-1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(0.75, system.Coefficients[0, 1], 1e-10);
        }

        [TestMethod]
        public void ParseInput_NegativeFractionalCoefficients_FirstConstant()
        {
            string input = "-1/2x1 + 3/4x2 = 5\nx1 - x2 = 1";
            var system = DataProcessor.ParseInput(input);
            Assert.AreEqual(5.0, system.Constants[0], 1e-10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_EmptyInput_ThrowsArgumentException()
        {
            string input = "";
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_NonLinearEquation_ThrowsArgumentException()
        {
            string input = "x1^2 + x2 = 5";
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_InconsistentVariableCount_ThrowsArgumentException()
        {
            string input = "x1 + x2 = 5\nx1 = 1";
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_InvalidVariableIndex_ThrowsException()
        {
            string input = "x-0 + x1 = 5";
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_MalformedTerm_ThrowsException()
        {
            string input = "2.2.2x1 + x2 = 5";
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_NoVariables_ThrowsException()
        {
            string input = "5 = 5";
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseInput_InvalidFraction_ThrowsException()
        {
            string input = "1/0x1 + x2 = 5";
            DataProcessor.ParseInput(input);
        }

        [TestMethod]
        public void SplitTerms_NegativeFirstTerm_ReturnsCorrectTerms()
        {
            string expression = "-x1+x2";
            var terms = DataProcessorTestsExtensions.SplitTerms(expression);
            Assert.AreEqual(2, terms.Count);
        }

        [TestMethod]
        public void ValidateInput_ValidSystem_DoesNotThrow()
        {
            var system = new DataProcessor.LinearSystem
            {
                Coefficients = new double[,] { { 2, 1 }, { 1, -1 } },
                Constants = new double[] { 5, 1 }
            };
            DataProcessor.ValidateInput(system);
            Assert.IsTrue(true); // Explicit Assert to confirm no exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateInput_ZeroRowWithNonZeroConstant_ThrowsArgumentException()
        {
            var system = new DataProcessor.LinearSystem
            {
                Coefficients = new double[,] { { 0, 0 }, { 1, -1 } },
                Constants = new double[] { 5, 1 }
            };
            DataProcessor.ValidateInput(system);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateInput_NullSystem_ThrowsException()
        {
            DataProcessor.ValidateInput(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateInput_EmptyMatrix_ThrowsException()
        {
            var system = new DataProcessor.LinearSystem
            {
                Coefficients = new double[0, 0],
                Constants = new double[0]
            };
            DataProcessor.ValidateInput(system);
        }
    }
}