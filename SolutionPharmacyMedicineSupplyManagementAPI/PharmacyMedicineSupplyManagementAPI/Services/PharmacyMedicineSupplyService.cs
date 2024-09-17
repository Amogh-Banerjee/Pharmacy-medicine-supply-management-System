using System.Text.Json;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Repositories;

namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public class PharmacyMedicineSupplyService: IPharmacyMedicineSupplyService
	{		

		private readonly IPharmacyMedicineSupplyRepo _repository;

		public PharmacyMedicineSupplyService(IPharmacyMedicineSupplyRepo repository)
		{			
			_repository = repository;
		}		

		public async Task<List<PharmacyMedicineSupply>> GetPharmacyMedicineSupplyAsync(List<MedicineDemandDto> demands, List<MedicineStock> allStock)
		{
			var pharmacies = await _repository.GetPharmaciesAsync();			

			var supplies = new List<PharmacyMedicineSupply>();			

			foreach (var demand in demands)
			{
				var stock = allStock.FirstOrDefault(s => s.MedId == demand.MedId);

				if (stock == null)
					continue;

				int totalStock = stock.NumberOfTabletsInStock;
				int demandCount = demand.DemandCount;
				int supplyPerPharmacy = totalStock >= demandCount ? demandCount / pharmacies.Count() : totalStock / pharmacies.Count();
				int remainingStock = totalStock >= demandCount ? demandCount % pharmacies.Count() : totalStock % pharmacies.Count();

				foreach (var pharmacy in pharmacies)
				{
					supplies.Add(new PharmacyMedicineSupply
					{
						PharmacyId = pharmacy.PharmacyId,
						MedId = demand.MedId,
						SupplyCount = supplyPerPharmacy,
						Med = stock,
						Pharmacy = pharmacy
					});
				}

				if (remainingStock > 0)
				{
					var firstSupply = supplies.FirstOrDefault(s => s.MedId == demand.MedId);
					if (firstSupply != null)
					{
						firstSupply.SupplyCount += remainingStock;
					}
				}

				var demandToSave = new MedicineDemand
				{
					MedId = demand.MedId,
					DemandCount = demand.DemandCount
				};
				_repository.AddMedicineDemand(demandToSave);
			}

            foreach (var supply in supplies)
            {
				_repository.AddPharmacyMedicineSupply(supply);
            }

            return supplies;
		}
	}
}
