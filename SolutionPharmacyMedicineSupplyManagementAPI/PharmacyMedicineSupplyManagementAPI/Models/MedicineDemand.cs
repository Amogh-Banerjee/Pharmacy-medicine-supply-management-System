using System;
using System.Collections.Generic;

namespace PharmacyMedicineSupplyManagementAPI.Models;

public partial class MedicineDemand
{
    public int MedId { get; set; }

    public int DemandCount { get; set; }

    public virtual MedicineStock Med { get; set; } = null!;
}
