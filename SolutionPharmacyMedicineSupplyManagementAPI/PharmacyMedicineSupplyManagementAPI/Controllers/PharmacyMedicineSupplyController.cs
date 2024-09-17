using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Services;

namespace PharmacyMedicineSupplyManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PharmacyMedicineSupplyController : ControllerBase
	{
		private readonly IPharmacyMedicineSupplyService _pharmacyMedicineSupplyService;

		public PharmacyMedicineSupplyController(IPharmacyMedicineSupplyService pharmacyMedicineSupplyService)
		{
			_pharmacyMedicineSupplyService = pharmacyMedicineSupplyService;
		}

		[HttpPost("PharmacySupply")]
		public async Task<ActionResult<List<PharmacyMedicineSupplyDto>>> GetPharmacyMedicineSupply([FromBody] DemandsAndAllStockDto demandsAndAllStock)
		{
			if (demandsAndAllStock.demands == null || demandsAndAllStock.demands.Count == 0)
			{                
                return BadRequest("Demand list cannot be null or empty.");
			}

			var result = await _pharmacyMedicineSupplyService.GetPharmacyMedicineSupplyAsync(demandsAndAllStock.demands, demandsAndAllStock.allStock);

			var output = result.Select(supply => new PharmacyMedicineSupplyDto
			{				
				PharmacyId = supply.PharmacyId,
				PharmacyName = supply.Pharmacy.PharmacyName,
				MedId = supply.MedId,
				MedName = supply.Med.MedName,
				SupplyCount = supply.SupplyCount
			});

			return Ok(output);
		}
	}
}
