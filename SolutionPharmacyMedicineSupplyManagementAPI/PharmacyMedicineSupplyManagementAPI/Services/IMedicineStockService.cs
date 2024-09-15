namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public interface IMedicineStockService<MedicineStock>
	{
		Task<List<MedicineStock>> GetMedicineStockInfoAsync();
		Task<List<string>> GetMedicinesByAilmentAsync(string ailment);
	}
}
