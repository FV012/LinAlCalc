using LinAlCalc.Controller;
using LinAlCalc.Solver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinAlCalc.Tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void SolveSystem_InvalidInput_ReturnsUnknownStatus()
        {
            var controller = new LinearSystemController();
            string input = "x1 + x2 = 5\nx1 = 1";
            var result = LinearSystemController.SolveSystem(input);
            Assert.AreEqual(SolutionStatus.Unknown, result.Status);
        }

        [TestMethod]
        public void SolveSystem_InvalidInput_HasEmptySolutions()
        {
            var controller = new LinearSystemController();
            string input = "x1 + x2 = 5\nx1 = 1";
            var result = LinearSystemController.SolveSystem(input);
            Assert.AreEqual(0, result.Solutions.Count);
        }

        [TestMethod]
        public void SolveSystem_InvalidInput_HasNaNResidualNorm()
        {
            var controller = new LinearSystemController();
            string input = "x1 + x2 = 5\nx1 = 1";
            var result = LinearSystemController.SolveSystem(input);
            Assert.IsTrue(double.IsNaN(result.ResidualNorm));
        }

        [TestMethod]
        public void ValidateInput_ValidInput_ReturnsTrue()
        {
            var controller = new LinearSystemController();
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            string errorMessage;
            bool isValid = LinearSystemController.ValidateInput(input, out errorMessage);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateInput_ValidInput_HasEmptyErrorMessage()
        {
            var controller = new LinearSystemController();
            string input = "2x1 + x2 = 5\nx1 - x2 = 1";
            string errorMessage;
            LinearSystemController.ValidateInput(input, out errorMessage);
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
        }

        [TestMethod]
        public void ValidateInput_InvalidInput_ReturnsFalse()
        {
            var controller = new LinearSystemController();
            string input = "x1 + x2 = 5\nx1 = 1";
            string errorMessage;
            bool isValid = LinearSystemController.ValidateInput(input, out errorMessage);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateInput_InvalidInput_HasErrorMessage()
        {
            var controller = new LinearSystemController();
            string input = "x1 + x2 = 5\nx1 = 1";
            string errorMessage;
            LinearSystemController.ValidateInput(input, out errorMessage);
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }
    }
}