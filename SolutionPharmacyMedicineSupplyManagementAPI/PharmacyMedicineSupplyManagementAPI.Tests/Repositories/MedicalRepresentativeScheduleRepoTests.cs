using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Repositories;

namespace PharmacyMedicineSupplyManagementAPI.Tests.Repositories
{
	internal class MedicalRepresentativeScheduleRepoTests
	{
		private Mock<MedDbContext> _mockContext;		
		private Mock<DbSet<RepSchedule>> _mockRepSchedulesSet;
		private MedicalRepresentativeScheduleRepo _repository;

		[SetUp]
		public void SetUp()
		{
			_mockContext = new Mock<MedDbContext>(); // Mock the DbContext			

			// Mock DbSet for RepSchedules
			_mockRepSchedulesSet = new Mock<DbSet<RepSchedule>>();
			_mockContext.Setup(c => c.RepSchedules).Returns(_mockRepSchedulesSet.Object);

			_repository = new MedicalRepresentativeScheduleRepo(_mockContext.Object, null); // Create repository instance
		}

		[Test]
		public void GetAllMedicalReps_ShouldReturnAllMedicalReps()
		{
			// Arrange
			var mockReps = new List<MedicalRep>
			{
				new MedicalRep { MedRepId = 1, MedRepName = "Rep 1" },
				new MedicalRep { MedRepId = 2, MedRepName = "Rep 2" }
			};

			var mockSet = new Mock<DbSet<MedicalRep>>();
			mockSet.As<IEnumerable<MedicalRep>>().Setup(m => m.GetEnumerator()).Returns(mockReps.GetEnumerator());

			var mockContext = new Mock<MedDbContext>();
			mockContext.Setup(c => c.MedicalReps).Returns(mockSet.Object);

			var repository = new MedicalRepresentativeScheduleRepo(mockContext.Object, null);


			// Act
			var result = repository.GetAllMedicalReps();

			// Assert
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual("Rep 1", result[0].MedRepName);
			Assert.AreEqual("Rep 2", result[1].MedRepName);
		}

		[Test]
		public void AddSchedule_ShouldAddRepSchedule()
		{
			// Arrange
			var schedule = new RepSchedule { SchId = 1, MedRepId = 1, MeetingDate = new DateOnly() };

			// Act
			_repository.AddSchedule(schedule);

			// Assert
			_mockRepSchedulesSet.Verify(m => m.Add(It.IsAny<RepSchedule>()), Times.Once());
			_mockContext.Verify(c => c.SaveChanges(), Times.Once());
		}
	}
}
