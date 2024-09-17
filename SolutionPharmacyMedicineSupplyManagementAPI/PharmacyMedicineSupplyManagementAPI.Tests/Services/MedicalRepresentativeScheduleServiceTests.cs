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
	internal class MedicalRepresentativeScheduleServiceTests
	{		
		private Mock<IMedicalRepresentativeScheduleRepo> _mockRepScheduleRepo;
		private MedicalRepresentativeScheduleService _service;

		[SetUp]
		public void SetUp()
		{			
			_mockRepScheduleRepo = new Mock<IMedicalRepresentativeScheduleRepo>();
			_service = new MedicalRepresentativeScheduleService(_mockRepScheduleRepo.Object);
		}

		[Test]
		public async Task GetMedicinesByAilmentAsync_ShouldReturnFilteredMedicines()
		{
			// Arrange
			var ailment = "Headache";
			var allMedicines = new List<MedicineStock>
			{
				new MedicineStock { MedName = "Aspirin", TargetAilment = "Headache" },
				new MedicineStock { MedName = "Paracetamol", TargetAilment = "Fever" }
			};			

			// Act
			var result = await _service.GetMedicinesByAilmentAsync(ailment, allMedicines);

			// Assert
			Assert.AreEqual(1, result.Count);
			Assert.Contains("Aspirin", result);
		}

		[Test]
		public async Task GetMedicinesByAilmentAsync_ShouldReturnEmptyList_WhenNoMedicinesMatchAilment()
		{
			// Arrange
			var ailment = "NonexistentAilment";
			var allMedicines = new List<MedicineStock>
			{
				new MedicineStock { MedName = "Aspirin", TargetAilment = "Headache" },
				new MedicineStock { MedName = "Paracetamol", TargetAilment = "Fever" }
			};			

			// Act
			var result = await _service.GetMedicinesByAilmentAsync(ailment, allMedicines);

			// Assert
			Assert.AreEqual(0, result.Count);
		}

		[Test]
		public async Task GenerateRepScheduleAsync_ShouldGenerateSchedule_WhenDoctorsAndRepsAvailable()
		{
			// Arrange
			var scheduleStartDate = new DateTime(2024, 9, 16);
			var doctors = new List<Doctor>
			{
				new Doctor { Name = "D1", TreatingAilment = "Orthopaedics", ContactNumber = "9884122113" },
				new Doctor { Name = "D2", TreatingAilment = "General", ContactNumber = "9884122114" }
			};
			var medicalReps = new List<MedicalRep>
			{
				new MedicalRep { MedRepId = 1, MedRepName = "R1" },
				new MedicalRep { MedRepId = 2, MedRepName = "R2" }
			};
			var medicines = new List<MedicineStock>
			{
				new MedicineStock { MedName = "Orthoherb", TargetAilment = "Orthopaedics" },
				new MedicineStock { MedName = "Gaviscon", TargetAilment = "General" }
			};

			_mockRepScheduleRepo.Setup(repo => repo.GetAllDoctors()).Returns(doctors);
			_mockRepScheduleRepo.Setup(repo => repo.GetAllMedicalReps()).Returns(medicalReps);			

			var scheduleList = new List<RepSchedule>();

			_mockRepScheduleRepo.Setup(repo => repo.AddSchedule(It.IsAny<RepSchedule>()))
								.Callback<RepSchedule>(scheduleList.Add);

			// Act
			var result = await _service.GenerateRepScheduleAsync(scheduleStartDate, medicines);

			// Assert
			Assert.AreEqual(5, result.Count);
			Assert.AreEqual("R1", result[0].MedRep.MedRepName);
			Assert.AreEqual("D1", result[0].DoctorName);
			Assert.AreEqual("Orthopaedics", result[0].TreatingAilment);
			Assert.AreEqual("Orthoherb", result[0].Medicine);
			Assert.AreEqual(new TimeOnly(13, 0, 0), result[0].MeetingStartTime);
			Assert.AreEqual(new TimeOnly(14, 0, 0), result[0].MeetingEndTime);
			Assert.AreEqual(DateOnly.FromDateTime(scheduleStartDate), result[0].MeetingDate);
		}

		[Test]
		public async Task GenerateRepScheduleAsync_ShouldSkipSundays()
		{
			// Arrange
			var scheduleStartDate = new DateTime(2024, 9, 15); // Sunday
			var doctors = new List<Doctor>
			{
				new Doctor { Name = "D1", TreatingAilment = "Orthopaedics", ContactNumber = "9884122113" }
			};
			var medicalReps = new List<MedicalRep>
			{
				new MedicalRep { MedRepId = 1, MedRepName = "R1" }
			};
			var medicines = new List<MedicineStock>
			{
				new MedicineStock { MedName = "Orthoherb", TargetAilment = "Orthopaedics" }
			};

			_mockRepScheduleRepo.Setup(repo => repo.GetAllDoctors()).Returns(doctors);
			_mockRepScheduleRepo.Setup(repo => repo.GetAllMedicalReps()).Returns(medicalReps);			

			var scheduleList = new List<RepSchedule>();

			_mockRepScheduleRepo.Setup(repo => repo.AddSchedule(It.IsAny<RepSchedule>()))
								.Callback<RepSchedule>(scheduleList.Add);

			// Act
			var result = await _service.GenerateRepScheduleAsync(scheduleStartDate, medicines);

			// Assert
			Assert.AreEqual(5, result.Count);
			Assert.AreEqual("R1", result[0].MedRep.MedRepName);
			Assert.AreEqual("D1", result[0].DoctorName);
			Assert.AreEqual("Orthopaedics", result[0].TreatingAilment);
			Assert.AreEqual("Orthoherb", result[0].Medicine);
			Assert.AreEqual(new TimeOnly(13, 0, 0), result[0].MeetingStartTime);
			Assert.AreEqual(new TimeOnly(14, 0, 0), result[0].MeetingEndTime);
			Assert.AreEqual(DateOnly.FromDateTime(scheduleStartDate.AddDays(1)), result[0].MeetingDate); // Skip Sunday
		}

		[Test]
		public async Task GenerateRepScheduleAsync_ShouldHandleNoMedicinesAvailable()
		{
			// Arrange
			var scheduleStartDate = new DateTime(2024, 9, 16);
			var doctors = new List<Doctor>
			{
				new Doctor { Name = "D1", TreatingAilment = "Orthopaedics", ContactNumber = "9884122113" }
			};
			var medicalReps = new List<MedicalRep>
			{
				new MedicalRep { MedRepId = 1, MedRepName = "R1" }
			};

			_mockRepScheduleRepo.Setup(repo => repo.GetAllDoctors()).Returns(doctors);
			_mockRepScheduleRepo.Setup(repo => repo.GetAllMedicalReps()).Returns(medicalReps);
			
			var scheduleList = new List<RepSchedule>();

			_mockRepScheduleRepo.Setup(repo => repo.AddSchedule(It.IsAny<RepSchedule>()))
								.Callback<RepSchedule>(scheduleList.Add);

			// Act
			var result = await _service.GenerateRepScheduleAsync(scheduleStartDate, new List<MedicineStock>());

			// Assert
			Assert.AreEqual(0, result.Count); // No schedule should be created
		}

		[Test]
		public async Task GenerateRepScheduleAsync_ShouldHandleNoDoctorsOrReps()
		{
			// Arrange
			var scheduleStartDate = new DateTime(2024, 9, 16);
			_mockRepScheduleRepo.Setup(repo => repo.GetAllDoctors()).Returns(new List<Doctor>());
			_mockRepScheduleRepo.Setup(repo => repo.GetAllMedicalReps()).Returns(new List<MedicalRep>());

			var scheduleList = new List<RepSchedule>();

			_mockRepScheduleRepo.Setup(repo => repo.AddSchedule(It.IsAny<RepSchedule>()))
								.Callback<RepSchedule>(scheduleList.Add);

			// Act
			var result = await _service.GenerateRepScheduleAsync(scheduleStartDate, new List<MedicineStock>());

			// Assert
			Assert.AreEqual(0, result.Count); // No schedule should be created
		}
	}
}
