using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyMedicineSupplyManagementAPI.Controllers;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Services;

namespace PharmacyMedicineSupplyManagementAPI.Tests.Controllers
{
	internal class PharmacyMedicineSupplyControllerTests
	{
		private Mock<IPharmacyMedicineSupplyService> _mockService;
		private PharmacyMedicineSupplyController _controller;

		[SetUp]
		public void Setup()
		{
			_mockService = new Mock<IPharmacyMedicineSupplyService>();
			_controller = new PharmacyMedicineSupplyController(_mockService.Object);
		}

		[Test]
		public async Task GetPharmacyMedicineSupply_ValidInput_ReturnsOkResult()
		{
			// Arrange
			var demands = new List<MedicineDemandDto> { new MedicineDemandDto { MedId = 1, DemandCount = 50 } };
			var stock = new List<MedicineStock> { new MedicineStock { MedId = 1, MedName = "Med1", NumberOfTabletsInStock = 100 } };

			var pharmacySupplies = new List<PharmacyMedicineSupply>
			{
				new PharmacyMedicineSupply
				{
					PharmacyId = 1,
					Pharmacy = new Pharmacy { PharmacyId = 1, PharmacyName = "Pharmacy1" },
					MedId = 1,
					Med = new MedicineStock { MedId = 1, MedName = "Med1" },
					SupplyCount = 50
				}
			};

			_mockService
				.Setup(service => service.GetPharmacyMedicineSupplyAsync(demands, stock))
				.ReturnsAsync(pharmacySupplies);

			var demandsAndAllStock = new DemandsAndAllStockDto { demands = demands, allStock = stock };

			// Act
			var result = await _controller.GetPharmacyMedicineSupply(demandsAndAllStock);

			// Assert
			var okResult = result.Result as OkObjectResult;
			Assert.IsNotNull(okResult);
			Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
		}

		[Test]
		public async Task GetPharmacyMedicineSupply_NullDemands_ReturnsBadRequest()
		{
			// Arrange
			var demandsAndAllStock = new DemandsAndAllStockDto { demands = null, allStock = new List<MedicineStock>() };

			// Act
			var result = await _controller.GetPharmacyMedicineSupply(demandsAndAllStock);

			// Assert
			var badRequestResult = result.Result as BadRequestObjectResult;
			Assert.IsNotNull(badRequestResult);
			Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
			Assert.AreEqual("Demand list cannot be null or empty.", badRequestResult.Value);
		}

		[Test]
		public async Task GetPharmacyMedicineSupply_EmptyDemands_ReturnsBadRequest()
		{
			// Arrange
			var demandsAndAllStock = new DemandsAndAllStockDto { demands = new List<MedicineDemandDto>(), allStock = new List<MedicineStock>() };

			// Act
			var result = await _controller.GetPharmacyMedicineSupply(demandsAndAllStock);

			// Assert
			var badRequestResult = result.Result as BadRequestObjectResult;
			Assert.IsNotNull(badRequestResult);
			Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
			Assert.AreEqual("Demand list cannot be null or empty.", badRequestResult.Value);
		}
	}
}
