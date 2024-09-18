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
	internal class PharmacyMedicineSupplyServiceTests
	{
		private PharmacyMedicineSupplyService _supplyService;
		private Mock<IPharmacyMedicineSupplyRepo> _mockSupplyRepo;

		[SetUp]
		public void SetUp()
		{
			_mockSupplyRepo = new Mock<IPharmacyMedicineSupplyRepo>();
			_supplyService = new PharmacyMedicineSupplyService(_mockSupplyRepo.Object);
		}

		[Test]
		public async Task GetPharmacyMedicineSupplyAsync_EnoughStock_DistributesEqually()
		{
			// Arrange
			var pharmacies = new List<Pharmacy>
			{
				new Pharmacy { PharmacyId = 1 },
				new Pharmacy { PharmacyId = 2 }
			};

			var allStock = new List<MedicineStock>
			{
				new MedicineStock { MedId = 1, NumberOfTabletsInStock = 100 }
			};

			var demands = new List<MedicineDemandDto>
			{
				new MedicineDemandDto { MedId = 1, DemandCount = 80 }
			};

			_mockSupplyRepo.Setup(r => r.GetPharmaciesAsync()).ReturnsAsync(pharmacies);

			// Act
			var result = await _supplyService.GetPharmacyMedicineSupplyAsync(demands, allStock);

			// Assert
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual(40, result[0].SupplyCount);
			Assert.AreEqual(40, result[1].SupplyCount);
			_mockSupplyRepo.Verify(r => r.AddMedicineDemand(It.IsAny<MedicineDemand>()), Times.Once);
			_mockSupplyRepo.Verify(r => r.AddPharmacyMedicineSupply(It.IsAny<PharmacyMedicineSupply>()), Times.Exactly(2));
		}

		[Test]
		public async Task GetPharmacyMedicineSupplyAsync_InsufficientStock_DistributesAllAvailableStock()
		{
			// Arrange
			var pharmacies = new List<Pharmacy>
			{
				new Pharmacy { PharmacyId = 1 },
				new Pharmacy { PharmacyId = 2 }
			};

			var allStock = new List<MedicineStock>
			{
				new MedicineStock { MedId = 1, NumberOfTabletsInStock = 50 }
			};

			var demands = new List<MedicineDemandDto>
			{
				new MedicineDemandDto { MedId = 1, DemandCount = 80 }
			};

			_mockSupplyRepo.Setup(r => r.GetPharmaciesAsync()).ReturnsAsync(pharmacies);

			// Act
			var result = await _supplyService.GetPharmacyMedicineSupplyAsync(demands, allStock);

			// Assert
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual(25, result[0].SupplyCount);
			Assert.AreEqual(25, result[1].SupplyCount);
			_mockSupplyRepo.Verify(r => r.AddMedicineDemand(It.IsAny<MedicineDemand>()), Times.Once);
			_mockSupplyRepo.Verify(r => r.AddPharmacyMedicineSupply(It.IsAny<PharmacyMedicineSupply>()), Times.Exactly(2));
		}

		[Test]
		public async Task GetPharmacyMedicineSupplyAsync_RemainingMedicines_DistributesRemaining()
		{
			// Arrange
			var pharmacies = new List<Pharmacy>
			{
				new Pharmacy { PharmacyId = 1 },
				new Pharmacy { PharmacyId = 2 }
			};

			var allStock = new List<MedicineStock>
			{
				new MedicineStock { MedId = 1, NumberOfTabletsInStock = 100 }
			};

			var demands = new List<MedicineDemandDto>
			{
				new MedicineDemandDto { MedId = 1, DemandCount = 63 }
			};

			_mockSupplyRepo.Setup(r => r.GetPharmaciesAsync()).ReturnsAsync(pharmacies);

			// Act
			var result = await _supplyService.GetPharmacyMedicineSupplyAsync(demands, allStock);

			// Assert
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual(32, result[0].SupplyCount); // First pharmacy gets extra tablet.
			Assert.AreEqual(31, result[1].SupplyCount);
			_mockSupplyRepo.Verify(r => r.AddMedicineDemand(It.IsAny<MedicineDemand>()), Times.Once);
			_mockSupplyRepo.Verify(r => r.AddPharmacyMedicineSupply(It.IsAny<PharmacyMedicineSupply>()), Times.Exactly(2));
		}
	}
}
