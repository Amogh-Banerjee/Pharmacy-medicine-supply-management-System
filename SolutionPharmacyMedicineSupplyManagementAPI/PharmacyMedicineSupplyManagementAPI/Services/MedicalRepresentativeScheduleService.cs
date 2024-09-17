using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Repositories;

namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public class MedicalRepresentativeScheduleService: IMedicalRepresentativeScheduleService
	{
		private readonly IMedicalRepresentativeScheduleRepo _repScheduleRepository;		
		public MedicalRepresentativeScheduleService(IMedicalRepresentativeScheduleRepo repScheduleRepository)
		{			
			_repScheduleRepository = repScheduleRepository;
		}

		public async Task<List<string>> GetMedicinesByAilmentAsync(string ailment, List<MedicineStock> allMedicines)
		{			
			return allMedicines
				.Where(m => m.TargetAilment.Equals(ailment, StringComparison.OrdinalIgnoreCase))
				.Select(m => m.MedName)
				.ToList();
		}

		public async Task<List<RepSchedule>> GenerateRepScheduleAsync(DateTime scheduleStartDate, List<MedicineStock> allMedicines)
		{
			// Load data
			var doctors = _repScheduleRepository.GetAllDoctors();
			var medicalReps = _repScheduleRepository.GetAllMedicalReps();

			if (!doctors.Any() || !medicalReps.Any())
			{
				// Handle the case where there are no doctors or medical reps
				return new List<RepSchedule>();
			}

			var scheduleList = new List<RepSchedule>();
			var currentDate = scheduleStartDate;
			int repIndex = 0;
			int maxAttempts = 5;

			// Generate a 5-day schedule excluding Sundays
			while (scheduleList.Count < 5 && maxAttempts > 0)
			{
				if (currentDate.DayOfWeek == DayOfWeek.Sunday)
				{
					currentDate = currentDate.AddDays(1);
					continue;
				}

				var doctor = doctors[scheduleList.Count % doctors.Count];
				var medicinesForAilment = await GetMedicinesByAilmentAsync(doctor.TreatingAilment, allMedicines);

				if (!medicinesForAilment.Any())
				{
					currentDate = currentDate.AddDays(1);
					maxAttempts--;
					continue;
				}

				var medRep = medicalReps[repIndex % medicalReps.Count];

				var schedule = new RepSchedule
				{
					MedRepId = medRep.MedRepId,
					MedRep = medRep,
					DoctorName = doctor.Name,
					TreatingAilment = doctor.TreatingAilment,
					Medicine = string.Join(", ", medicinesForAilment),
					MeetingStartTime = new TimeOnly(13, 0, 0),
					MeetingEndTime = new TimeOnly(14, 0, 0),
					MeetingDate = DateOnly.FromDateTime(currentDate),
					DoctorContact = doctor.ContactNumber
				};

				// Save the schedule via repository
				_repScheduleRepository.AddSchedule(schedule);
				scheduleList.Add(schedule);

				repIndex++;
				currentDate = currentDate.AddDays(1);
			}

			return scheduleList;
		}
	}
}
