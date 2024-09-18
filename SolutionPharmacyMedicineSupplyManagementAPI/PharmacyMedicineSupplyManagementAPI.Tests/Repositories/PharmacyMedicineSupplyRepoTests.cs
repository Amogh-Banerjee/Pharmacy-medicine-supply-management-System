using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Repositories;

namespace PharmacyMedicineSupplyManagementAPI.Tests.Repositories
{
	internal class PharmacyMedicineSupplyRepoTests
	{
		private Mock<MedDbContext> _mockContext;
		private PharmacyMedicineSupplyRepo _repo;

		[SetUp]
		public void SetUp()
		{
			_mockContext = new Mock<MedDbContext>();
			_repo = new PharmacyMedicineSupplyRepo(_mockContext.Object);
		}

		[Test]
		public async Task GetPharmaciesAsync_ReturnsListOfPharmacies()
		{
			// Arrange
			var pharmacies = new List<Pharmacy>
			{
				new Pharmacy { PharmacyId = 1, PharmacyName = "Pharmacy 1" },
				new Pharmacy { PharmacyId = 2, PharmacyName = "Pharmacy 2" }
			};

			// Use Moq.EntityFrameworkCore to mock DbSet and async queries
			_mockContext.Setup(x => x.Pharmacies).ReturnsDbSet(pharmacies);

			// Act
			var result = await _repo.GetPharmaciesAsync();

			// Assert
			Assert.AreEqual(2, result.Count());
		}

		[Test]
		public void AddPharmacyMedicineSupply_SuccessfullyAddsSupply()
		{
			// Arrange
			var supply = new PharmacyMedicineSupply
			{
				PharmacyId = 1,
				MedId = 1,
				Med = new MedicineStock 
				{ 
					MedId = 1, 
					MedName = "Med1", 
					ChemicalComposition = "Chemicals",
					DateOfExpiry = new DateOnly(2025, 3, 2),
					NumberOfTabletsInStock = 100,
					TargetAilment = "Some Disease",					
				},
				SupplyCount = 10
			};

			var mockSet = new Mock<DbSet<PharmacyMedicineSupply>>();
			_mockContext.Setup(c => c.PharmacyMedicineSupplies).Returns(mockSet.Object);

			var mockRepo = new Mock<PharmacyMedicineSupplyRepo>(_mockContext.Object);
			mockRepo.Setup(r => r.DetachMedicineStock(It.IsAny<MedicineStock>()));

			// Act
			mockRepo.Object.AddPharmacyMedicineSupply(supply);

			// Assert
			mockSet.Verify(m => m.Add(It.IsAny<PharmacyMedicineSupply>()), Times.Once());
			_mockContext.Verify(m => m.SaveChanges(), Times.Once());
			mockRepo.Verify(r => r.DetachMedicineStock(It.IsAny<MedicineStock>()), Times.Once());  // Ensure the method is called
		}

		[Test]
		public void AddMedicineDemand_SuccessfullyAddsDemand()
		{
			// Arrange
			var demand = new MedicineDemand { MedId = 1, DemandCount = 100 };

			var mockSet = new Mock<DbSet<MedicineDemand>>();
			_mockContext.Setup(c => c.MedicineDemands).Returns(mockSet.Object);

			// Act
			_repo.AddMedicineDemand(demand);

			// Assert
			mockSet.Verify(m => m.Add(It.IsAny<MedicineDemand>()), Times.Once());
			_mockContext.Verify(m => m.SaveChanges(), Times.Once());
		}



	}
}
