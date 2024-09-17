namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public interface IMedicineStockService<MedicineStock>
	{
		Task<List<MedicineStock>> GetMedicineStockInfoAsync();		
	}
}
