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
	internal class MedicalRepresentativeScheduleControllerTests
	{
		private Mock<IMedicalRepresentativeScheduleService> _mockService;
		private MedicalRepresentativeScheduleController _controller;

		[SetUp]
		public void Setup()
		{
			_mockService = new Mock<IMedicalRepresentativeScheduleService>();
			_controller = new MedicalRepresentativeScheduleController(_mockService.Object);
		}

		[Test]
		public async Task GetRepScheduleAsync_ShouldReturnOkResult_WithSchedule()
		{
			// Arrange
			var scheduleStartDate = new DateTime(2024, 9, 16);
			var mockSchedule = new List<RepSchedule>
			{
				new RepSchedule
				{
					MedRep = new MedicalRep { MedRepName = "R1" },
					DoctorName = "D1",
					TreatingAilment = "Orthopaedics",
					Medicine = "Orthoherb, Cholecalciferol",
					MeetingStartTime = new TimeOnly(13, 0),
					MeetingEndTime = new TimeOnly(14, 0),
					MeetingDate = DateOnly.FromDateTime(scheduleStartDate),
					DoctorContact = "9884122113"
				}
			};

			_mockService.Setup(service => service.GenerateRepScheduleAsync(scheduleStartDate))
						.ReturnsAsync(mockSchedule);

			// Act
			var result = await _controller.GetRepScheduleAsync(scheduleStartDate) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result, "Result should not be null");
			Assert.AreEqual(200, result.StatusCode);

			var output = result.Value as IEnumerable<RepScheduleDto>;
			Assert.IsNotNull(output, "Output should not be null");

			var firstItem = output.FirstOrDefault();
			Assert.IsNotNull(firstItem, "First item in the output should not be null");

			// Assert each value
			Assert.AreEqual("R1", firstItem.RepName);
			Assert.AreEqual("D1", firstItem.DoctorName);
			Assert.AreEqual("Orthopaedics", firstItem.TreatingAilment);
			Assert.AreEqual("Orthoherb, Cholecalciferol", firstItem.Medicine);
			Assert.AreEqual("01:00 PM to 02:00 PM", firstItem.Slot);
			Assert.AreEqual("16-Sep-2024", firstItem.Date);
			Assert.AreEqual("9884122113", firstItem.DoctorContact);

		}

		[Test]
		public async Task GetRepScheduleAsync_ShouldReturnNotFound_WhenNoScheduleIsGenerated()
		{
			// Arrange
			var scheduleStartDate = new DateTime(2024, 9, 16);
			_mockService.Setup(service => service.GenerateRepScheduleAsync(scheduleStartDate))
						.ReturnsAsync(new List<RepSchedule>());

			// Act
			var result = await _controller.GetRepScheduleAsync(scheduleStartDate) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);

			var output = result.Value as IEnumerable<dynamic>;
			Assert.IsNotNull(output);
			Assert.IsEmpty(output);
		}

		[Test]
		public void GetRepScheduleAsync_ShouldReturnBadRequest_WhenExceptionIsThrown()
		{
			// Arrange
			var scheduleStartDate = new DateTime(2024, 9, 16);
			_mockService.Setup(service => service.GenerateRepScheduleAsync(scheduleStartDate))
						.ThrowsAsync(new Exception("Test exception"));

			// Act
			var result = _controller.GetRepScheduleAsync(scheduleStartDate).Result as ObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(500, result.StatusCode);
			Assert.AreEqual("Internal server error: Test exception", result.Value);
		}
	}
}
