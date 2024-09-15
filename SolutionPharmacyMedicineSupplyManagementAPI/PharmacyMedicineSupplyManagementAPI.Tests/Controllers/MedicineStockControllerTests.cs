using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyMedicineSupplyManagementAPI.Controllers;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Services;

namespace PharmacyMedicineSupplyManagementAPI.Tests.Controllers
{
	internal class MedicineStockControllerTests
	{
		private Mock<IMedicineStockService<MedicineStock>> _mockService;
		private MedicineStockController _controller;

		[SetUp]
		public void SetUp()
		{
			_mockService = new Mock<IMedicineStockService<MedicineStock>>();
			_controller = new MedicineStockController(_mockService.Object);
		}

		[Test]
		public async Task GetMedicineStockInfo_ReturnsOk_WithExpectedData()
		{
			// Arrange: Define the expected result
			var expectedStock = new List<MedicineStock>
		{
			new MedicineStock { MedId = 1, MedName = "MedicineA", ChemicalComposition = "Chem1, Chem2", 
				TargetAilment = "Ailment1",	DateOfExpiry = new DateOnly(2024, 12, 2), NumberOfTabletsInStock = 10 },
			new MedicineStock { MedId = 2, MedName = "MedicineB", ChemicalComposition = "Chem3, Chem4", 
				TargetAilment = "Ailment2", DateOfExpiry = new DateOnly(2024, 12, 5), NumberOfTabletsInStock = 20 }
		};

			// Mock the service to return the expected stock
			_mockService.Setup(service => service.GetMedicineStockInfoAsync()).ReturnsAsync(expectedStock);

			// Act: Call the controller method
			var result = await _controller.GetMedicineStockInfoAsync();

			// Assert: Verify that the result is an OkObjectResult and contains the expected data
			var okResult = result as OkObjectResult;
			Assert.IsNotNull(okResult); // Check that the result is not null
			Assert.AreEqual(200, okResult.StatusCode); // Check for HTTP 200 OK
			Assert.AreEqual(expectedStock, okResult.Value); // Check if the returned value matches the expected data
		}

		[Test]
		public async Task GetMedicineStockInfo_ReturnsOk_WithEmptyList_WhenNoData()
		{
			// Arrange: Mock an empty list from the service
			_mockService.Setup(service => service.GetMedicineStockInfoAsync()).ReturnsAsync(new List<MedicineStock>());

			// Act: Call the controller method
			var result = await _controller.GetMedicineStockInfoAsync();

			// Assert: Verify that the result is OkObjectResult with an empty list
			var okResult = result as OkObjectResult;
			Assert.IsNotNull(okResult);
			Assert.AreEqual(200, okResult.StatusCode);
			Assert.IsInstanceOf<List<MedicineStock>>(okResult.Value);
			var returnedList = okResult.Value as List<MedicineStock>;
			Assert.AreEqual(0, returnedList.Count); // Ensure it's an empty list
		}
	}
}
