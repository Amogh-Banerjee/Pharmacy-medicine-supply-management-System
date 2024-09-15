using System;
using System.Collections.Generic;

namespace PharmacyMedicineSupplyManagementAPI.Models;

public partial class Pharmacy
{
    public int PharmacyId { get; set; }

    public string PharmacyName { get; set; } = null!;

    public virtual ICollection<PharmacyMedicineSupply> PharmacyMedicineSupplies { get; set; } = new List<PharmacyMedicineSupply>();
}
