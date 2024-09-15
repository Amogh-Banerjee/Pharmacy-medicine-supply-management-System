using PharmacyMedicineSupplyManagementAPI.Models;

namespace PharmacyMedicineSupplyManagementAPI.Repositories
{
	public interface IMedicalRepresentativeScheduleRepo
	{
		List<Doctor> GetAllDoctors();
		List<MedicalRep> GetAllMedicalReps();
		void AddSchedule(RepSchedule schedule);
	}
}
