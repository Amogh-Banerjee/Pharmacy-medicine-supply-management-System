using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Services;

namespace PharmacyMedicineSupplyManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedicineStockController : ControllerBase
	{
		private readonly IMedicineStockService<MedicineStock> _service;
		public MedicineStockController(IMedicineStockService<MedicineStock> service)
		{
			_service = service;
		}

		[HttpGet("MedicineStockInformation")]
		public async Task<IActionResult> GetMedicineStockInfoAsync()
		{
			try
			{
				var medicines = await _service.GetMedicineStockInfoAsync();
				return Ok(medicines);
			}
			catch (Exception ex) {
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
