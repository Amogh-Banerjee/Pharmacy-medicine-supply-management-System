using PharmacyMedicineSupplyManagementAPI.Models;

namespace PharmacyMedicineSupplyManagementAPI.Repositories
{
    public interface IPharmacyMedicineSupplyRepo
    {
		Task<IEnumerable<Pharmacy>> GetPharmaciesAsync();
		void AddPharmacyMedicineSupply(PharmacyMedicineSupply supply);
		void AddMedicineDemand(MedicineDemand demand);
	}
}
