using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Services;

namespace PharmacyMedicineSupplyManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedicalRepresentativeScheduleController : ControllerBase
	{
		private readonly IMedicalRepresentativeScheduleService _service;
		public MedicalRepresentativeScheduleController(IMedicalRepresentativeScheduleService service)
		{
			_service = service;
		}

		[Authorize]
		[HttpPost]
		[Route("RepSchedule")]
		public async Task<IActionResult> GetRepScheduleAsync([FromQuery] DateTime scheduleStartDate, [FromBody] List<MedicineStock> allMedicines)
		{
			if (scheduleStartDate == DateTime.MinValue)
			{
				return BadRequest("Start date is required.");
			}

			// Ensure the date is not in the past
			if (scheduleStartDate < DateTime.Today)
			{
				return BadRequest("Start date cannot be in the past.");
			}

			try
			{
				var schedule = await _service.GenerateRepScheduleAsync(scheduleStartDate, allMedicines);

				var output = schedule.Select(s => new RepScheduleDto
				{
					RepName = s.MedRep.MedRepName,
					DoctorName = s.DoctorName,
					TreatingAilment = s.TreatingAilment,
					Medicine = s.Medicine,
					Slot = $"{s.MeetingStartTime} to {s.MeetingEndTime}",
					Date = s.MeetingDate.ToString("dd-MMM-yyyy"),
					DoctorContact = s.DoctorContact
				});

				return Ok(output);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
