namespace PharmacyMedicineSupplyManagementAPI.Models
{
	public class PharmacyMedicineSupplyDto
	{		
		public int PharmacyId { get; set; }		
		public string PharmacyName {  get; set; }
		public int MedId { get; set; }
		public string MedName { get; set; }
		public int SupplyCount { get; set; }
	}
}
