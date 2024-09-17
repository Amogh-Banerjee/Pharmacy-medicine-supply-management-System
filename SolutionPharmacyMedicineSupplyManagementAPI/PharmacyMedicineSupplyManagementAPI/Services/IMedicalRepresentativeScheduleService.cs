using PharmacyMedicineSupplyManagementAPI.Models;

namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public interface IMedicalRepresentativeScheduleService
	{
		Task<List<string>> GetMedicinesByAilmentAsync(string ailment, List<MedicineStock> allMedicines);
		Task<List<RepSchedule>> GenerateRepScheduleAsync(DateTime scheduleStartDate, List<MedicineStock> allMedicines);
	}
}
