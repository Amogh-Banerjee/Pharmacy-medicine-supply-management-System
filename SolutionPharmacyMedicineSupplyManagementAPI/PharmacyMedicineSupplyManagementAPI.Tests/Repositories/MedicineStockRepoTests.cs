using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Moq.EntityFrameworkCore;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Repositories;

namespace PharmacyMedicineSupplyManagementAPI.Tests.Repositories
{
	internal class MedicineStockRepoTests
	{
		private Mock<MedDbContext> _medContextMock;
		private MedicineStockRepo _medStockRepo;

		[SetUp]
		public void SetUp()
		{
			_medContextMock = new Mock<MedDbContext>(new DbContextOptions<MedDbContext>());
			_medStockRepo = new MedicineStockRepo(_medContextMock.Object);
		}

		[Test]
		public async Task TestGetMedicineStockInfo()
		{
			var medStockList = new List<MedicineStock>
			{
				new MedicineStock
				{
					MedId = 1,
					MedName = "Cholecalciferol",
					ChemicalComposition = "Cholecalciferol (Vitamin D3)",
					TargetAilment = "Orthopaedics",
					DateOfExpiry = new DateOnly(2025, 6, 15),
					NumberOfTabletsInStock = 200
				},
				new MedicineStock
				{
					MedId = 2,
					MedName = "Gaviscon",
					ChemicalComposition = "Sodium alginate, Sodium bicarbonate, Calcium carbonate",
					TargetAilment = "General",
					DateOfExpiry = new DateOnly(2025, 3, 1),
					NumberOfTabletsInStock = 150
				}
			};

			// Use Moq.EntityFrameworkCore to mock DbSet and async queries
			_medContextMock.Setup(x => x.MedicineStocks).ReturnsDbSet(medStockList);

			var actualResult = await _medStockRepo.GetMedicineStockInfoAsync();

			Assert.AreEqual(2, actualResult.Count);
		}
	}	
}
