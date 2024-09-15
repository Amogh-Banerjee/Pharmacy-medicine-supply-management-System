using System;
using System.Collections.Generic;

namespace PharmacyMedicineSupplyManagementAPI.Models;

public partial class MedicineStock
{
    public int MedId { get; set; }

    public string MedName { get; set; } = null!;

    public string ChemicalComposition { get; set; } = null!;

    public string TargetAilment { get; set; } = null!;

    public DateOnly DateOfExpiry { get; set; }

    public int NumberOfTabletsInStock { get; set; }

    public virtual MedicineDemand? MedicineDemand { get; set; }

    public virtual ICollection<PharmacyMedicineSupply> PharmacyMedicineSupplies { get; set; } = new List<PharmacyMedicineSupply>();
}
