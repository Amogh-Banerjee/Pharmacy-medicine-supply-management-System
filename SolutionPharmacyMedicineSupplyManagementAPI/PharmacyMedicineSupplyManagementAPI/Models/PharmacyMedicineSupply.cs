using System;
using System.Collections.Generic;

namespace PharmacyMedicineSupplyManagementAPI.Models;

public partial class PharmacyMedicineSupply
{
    public int SupplyId { get; set; }

    public int PharmacyId { get; set; }

    public int MedId { get; set; }

    public int SupplyCount { get; set; }

    public virtual MedicineStock Med { get; set; } = null!;

    public virtual Pharmacy Pharmacy { get; set; } = null!;
}
