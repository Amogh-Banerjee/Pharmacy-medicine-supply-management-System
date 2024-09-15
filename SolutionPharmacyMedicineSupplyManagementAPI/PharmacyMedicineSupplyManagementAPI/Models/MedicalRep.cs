using System;
using System.Collections.Generic;

namespace PharmacyMedicineSupplyManagementAPI.Models;

public partial class MedicalRep
{
    public int MedRepId { get; set; }

    public string MedRepName { get; set; } = null!;

    public virtual ICollection<RepSchedule> RepSchedules { get; set; } = new List<RepSchedule>();
}
