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

				// Check if the PharmacyID and MedID combination already exists
				var existingSupply = _context.PharmacyMedicineSupplies
					.FirstOrDefault(s => s.PharmacyId == supply.PharmacyId && s.MedId == supply.MedId);

				if (existingSupply != null)
				{
					// Update the existing record
					existingSupply.SupplyCount = supply.SupplyCount;
					_context.PharmacyMedicineSupplies.Update(existingSupply);
				}
				else
				{
					// Add a new record
					_context.PharmacyMedicineSupplies.Add(supply);
				}

				// Save changes
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
				// Check if the MedicineDemand record already exists
				var existingDemand = _context.MedicineDemands
					.FirstOrDefault(md => md.MedId == demand.MedId);

				if (existingDemand != null)
				{
					// If the record exists, update the DemandCount
					existingDemand.DemandCount = demand.DemandCount;
					_context.MedicineDemands.Update(existingDemand);
				}
				else
				{
					// If the record doesn't exist, add a new one
					_context.MedicineDemands.Add(demand);
				}

				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				throw;
			}
		}
	}
}
