using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Repositories;

namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public class MedicineStockService: IMedicineStockService<MedicineStock>
	{
		private readonly IMedicineStockRepo<MedicineStock> _repo;
		public MedicineStockService(IMedicineStockRepo<MedicineStock> repo)
		{
			_repo = repo;
		}

		public async Task<List<MedicineStock>> GetMedicineStockInfoAsync()
		{
			return await _repo.GetMedicineStockInfoAsync();
		}
		
	}
}
