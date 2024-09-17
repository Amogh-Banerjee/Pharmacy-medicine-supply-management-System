using PharmacyMedicineSupplyManagementAPI.Models;

namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public interface IPharmacyMedicineSupplyService
	{		
		Task<List<PharmacyMedicineSupply>> GetPharmacyMedicineSupplyAsync(List<MedicineDemandDto> demands, List<MedicineStock> allStock);
	}
}
