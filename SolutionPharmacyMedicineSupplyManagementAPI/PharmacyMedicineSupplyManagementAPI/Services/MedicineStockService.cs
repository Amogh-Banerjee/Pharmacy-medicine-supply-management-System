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

		public async Task<List<string>> GetMedicinesByAilmentAsync(string ailment)
		{
			// Assuming that GetMedicineStockInfoAsync is called before this method.
			var allMedicines = await GetMedicineStockInfoAsync();
			return allMedicines
				.Where(m => m.TargetAilment.Equals(ailment, StringComparison.OrdinalIgnoreCase))
				.Select(m => m.MedName)
				.ToList();
		}
	}
}
