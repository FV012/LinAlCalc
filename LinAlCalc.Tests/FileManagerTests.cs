using LinAlCalc.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinAlCalc.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        private string GetTempFilePath()
        {
            return Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
        }

        [TestMethod]
        public async Task SaveInputAsync_ValidInput_CreatesFile()
        {
            var fileManager = new FileManager();
            string input = "2x1 + x2 = 5";
            string filePath = GetTempFilePath();
            await fileManager.SaveInputAsync(input, filePath);
            Assert.IsTrue(File.Exists(filePath));
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SaveInputAsync_EmptyInput_ThrowsException()
        {
            var fileManager = new FileManager();
            string input = "";
            string filePath = GetTempFilePath();
            await fileManager.SaveInputAsync(input, filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SaveInputAsync_EmptyPath_ThrowsException()
        {
            var fileManager = new FileManager();
            string input = "2x1 + x2 = 5";
            string filePath = "";
            await fileManager.SaveInputAsync(input, filePath);
        }

        [TestMethod]
        public async Task ReadInputAsync_ValidFile_ReturnsContent()
        {
            var fileManager = new FileManager();
            string input = "2x1 + x2 = 5";
            string filePath = GetTempFilePath();
            await File.WriteAllTextAsync(filePath, input);
            string content = await fileManager.ReadInputAsync(filePath);
            Assert.AreEqual(input, content);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public async Task ReadInputAsync_NonExistentFile_ThrowsException()
        {
            var fileManager = new FileManager();
            string filePath = GetTempFilePath();
            await fileManager.ReadInputAsync(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ReadInputAsync_EmptyPath_ThrowsException()
        {
            var fileManager = new FileManager();
            string filePath = "";
            await fileManager.ReadInputAsync(filePath);
        }

        [TestMethod]
        public async Task SaveResultAsync_ValidResult_CreatesFile()
        {
            var fileManager = new FileManager();
            string result = "x1 = 2\nx2 = 1";
            string filePath = GetTempFilePath();
            await fileManager.SaveResultAsync(result, filePath);
            Assert.IsTrue(File.Exists(filePath));
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SaveResultAsync_EmptyResult_ThrowsException()
        {
            var fileManager = new FileManager();
            string result = "";
            string filePath = GetTempFilePath();
            await fileManager.SaveResultAsync(result, filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SaveResultAsync_EmptyPath_ThrowsException()
        {
            var fileManager = new FileManager();
            string result = "x1 = 2\nx2 = 1";
            string filePath = "";
            await fileManager.SaveResultAsync(result, filePath);
        }

        [TestMethod]
        public void FileExists_ExistingFile_ReturnsTrue()
        {
            var fileManager = new FileManager();
            string filePath = GetTempFilePath();
            File.WriteAllText(filePath, "test");
            bool exists = fileManager.FileExists(filePath);
            Assert.IsTrue(exists);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        [TestMethod]
        public void FileExists_NonExistingFile_ReturnsFalse()
        {
            var fileManager = new FileManager();
            string filePath = GetTempFilePath();
            bool exists = fileManager.FileExists(filePath);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void FileExists_EmptyPath_ReturnsFalse()
        {
            var fileManager = new FileManager();
            string filePath = "";
            bool exists = fileManager.FileExists(filePath);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void IsDirectoryWritable_WritableDirectory_ReturnsTrue()
        {
            var fileManager = new FileManager();
            string directoryPath = Path.GetTempPath();
            bool isWritable = fileManager.IsDirectoryWritable(directoryPath);
            Assert.IsTrue(isWritable);
        }

        [TestMethod]
        public void IsDirectoryWritable_EmptyPath_ReturnsFalse()
        {
            var fileManager = new FileManager();
            string directoryPath = "";
            bool isWritable = fileManager.IsDirectoryWritable(directoryPath);
            Assert.IsFalse(isWritable);
        }
    }
}
