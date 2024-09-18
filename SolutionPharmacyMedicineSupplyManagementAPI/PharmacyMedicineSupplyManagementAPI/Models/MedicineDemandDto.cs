using System.ComponentModel.DataAnnotations;

namespace PharmacyMedicineSupplyManagementAPI.Models
{
	public class MedicineDemandDto
	{
		[Required(ErrorMessage = "Medicine ID is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "Medicine ID must be greater than 0.")]
		public int MedId { get; set; }

		[Required(ErrorMessage = "Demand count is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "Demand count must be greater than 0.")]
		public int DemandCount { get; set; }
	}
}
