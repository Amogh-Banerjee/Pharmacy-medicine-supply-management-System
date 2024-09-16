using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyMedicineSupplyManagementAPI.Services;

namespace PharmacyMedicineSupplyManagementAPI.Tests.Services
{
	internal class FileReaderTests
	{
		private FileReader _fileReader;
		private string _tempFilePath;

		[SetUp]
		public void SetUp()
		{
			_fileReader = new FileReader();
			_tempFilePath = Path.GetTempFileName();
		}

		[TearDown]
		public void TearDown()
		{
			if (File.Exists(_tempFilePath))
			{
				File.Delete(_tempFilePath);
			}
		}

		[Test]
		public void FileExists_ShouldReturnTrue_WhenFileExists()
		{
			// Act
			var result = _fileReader.FileExists(_tempFilePath);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void FileExists_ShouldReturnFalse_WhenFileDoesNotExist()
		{
			// Arrange
			var filePath = Path.Combine(Path.GetTempPath(), "nonexistentfile.txt");

			// Act
			var result = _fileReader.FileExists(filePath);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void ReadFile_ShouldReturnContent_WhenFileExists()
		{
			// Arrange
			var expectedContent = "Test content";
			File.WriteAllText(_tempFilePath, expectedContent);

			// Act
			var result = _fileReader.ReadFile(_tempFilePath);

			// Assert
			Assert.AreEqual(expectedContent, result);
		}

		[Test]
		public void ReadFile_ShouldThrowException_WhenFileDoesNotExist()
		{
			// Arrange
			var filePath = Path.Combine(Path.GetTempPath(), "nonexistentfile.txt");

			// Act & Assert
			Assert.Throws<FileNotFoundException>(() => _fileReader.ReadFile(filePath));
		}

	}
}
