using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Repositories;
using PharmacyMedicineSupplyManagementAPI.Services;

namespace PharmacyMedicineSupplyManagementAPI.Tests.Services
{
	internal class MedicineStockServiceTests
	{
		private MedicineStockService _myService;
		private Mock<IMedicineStockRepo<MedicineStock>> _mockRepository;

		[SetUp]
		public void SetUp()
		{
			_mockRepository = new Mock<IMedicineStockRepo<MedicineStock>>();
			_myService = new MedicineStockService(_mockRepository.Object);
		}

		[Test]
		public async Task GetMedicineStockInfoAsync_ReturnsExpectedStock()
		{
			// Arrange: Define expected result
			var expectedStock = new List<MedicineStock>
		{
			new MedicineStock { MedId = 1, MedName = "MedicineA", ChemicalComposition = "Chem1, Chem2", TargetAilment = "Ailment1",
				DateOfExpiry = new DateOnly(2024, 12, 2), NumberOfTabletsInStock = 10 },
			new MedicineStock { MedId = 2, MedName = "MedicineB", ChemicalComposition = "Chem3, Chem4", TargetAilment = "Ailment2",
				DateOfExpiry = new DateOnly(2024, 12, 5), NumberOfTabletsInStock = 20 }
		};

			// Mock the repository to return the expected stock
			_mockRepository.Setup(repo => repo.GetMedicineStockInfoAsync()).ReturnsAsync(expectedStock);

			// Act: Call the service method
			var result = await _myService.GetMedicineStockInfoAsync();

			// Assert: Verify the result matches the expected data
			Assert.AreEqual(expectedStock, result);
			Assert.AreEqual(2, result.Count); // Additional check
		}

		[Test]
		public async Task GetMedicineStockInfoAsync_ReturnsEmptyList_WhenNoData()
		{
			// Arrange: Mock an empty list from the repository
			_mockRepository.Setup(repo => repo.GetMedicineStockInfoAsync()).ReturnsAsync(new List<MedicineStock>());

			// Act: Call the service method
			var result = await _myService.GetMedicineStockInfoAsync();

			// Assert: Check that the result is an empty list
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Count);
		}
	}
}
