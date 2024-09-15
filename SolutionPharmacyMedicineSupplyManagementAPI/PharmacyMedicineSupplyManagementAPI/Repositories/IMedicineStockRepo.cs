using Microsoft.AspNetCore.Mvc;

namespace PharmacyMedicineSupplyManagementAPI.Repositories
{
	public interface IMedicineStockRepo<MedicineStock>
	{
		Task<List<MedicineStock>> GetMedicineStockInfoAsync();
	}
}
