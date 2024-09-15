using PharmacyMedicineSupplyManagementAPI.Models;

namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public interface IMedicalRepresentativeScheduleService
	{
		Task<List<RepSchedule>> GenerateRepScheduleAsync(DateTime scheduleStartDate);
	}
}
