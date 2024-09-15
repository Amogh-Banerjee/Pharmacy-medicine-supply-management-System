using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupplyManagementAPI.Models;

namespace PharmacyMedicineSupplyManagementAPI.Repositories
{
	public class MedicineStockRepo: IMedicineStockRepo<MedicineStock>
	{
		private readonly MedDbContext _context;
		public MedicineStockRepo(MedDbContext context)
		{
			_context = context;
		}

		public async Task<List<MedicineStock>> GetMedicineStockInfoAsync()
		{
			return await _context.MedicineStocks.ToListAsync();
		}
	}
}
