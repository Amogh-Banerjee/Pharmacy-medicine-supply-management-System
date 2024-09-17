namespace PharmacyMedicineSupplyManagementAPI.Models
{
	public class DemandsAndAllStockDto
	{
		public List<MedicineDemandDto> demands {  get; set; }
		public List<MedicineStock> allStock {  get; set; }

	}
}
