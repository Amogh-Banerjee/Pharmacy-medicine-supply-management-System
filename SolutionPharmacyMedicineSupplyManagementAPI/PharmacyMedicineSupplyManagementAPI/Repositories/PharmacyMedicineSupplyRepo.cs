using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupplyManagementAPI.Models;

namespace PharmacyMedicineSupplyManagementAPI.Repositories
{
	public class PharmacyMedicineSupplyRepo: IPharmacyMedicineSupplyRepo
	{
		private readonly MedDbContext _context;

		public PharmacyMedicineSupplyRepo(MedDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Pharmacy>> GetPharmaciesAsync()
		{
			return await _context.Pharmacies.ToListAsync();
		}

		public void AddPharmacyMedicineSupply(PharmacyMedicineSupply supply)
		{
			try
			{
				DetachMedicineStock(supply.Med);

				_context.PharmacyMedicineSupplies.Add(supply);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				throw;
			}
		}

		public virtual void DetachMedicineStock(MedicineStock med)
		{
			// Detach the MedicineStock to prevent EF from trying to insert it
			_context.Entry(med).State = EntityState.Unchanged;
		}

		public void AddMedicineDemand(MedicineDemand demand)
		{
			try
			{
				_context.MedicineDemands.Add(demand);
				_context.SaveChanges();
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				throw;
			}
		}
	}
}
