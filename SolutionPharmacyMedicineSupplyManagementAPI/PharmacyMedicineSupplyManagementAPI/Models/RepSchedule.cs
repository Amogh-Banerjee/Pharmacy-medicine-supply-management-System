using System;
using System.Collections.Generic;

namespace PharmacyMedicineSupplyManagementAPI.Models;

public partial class RepSchedule
{
    public int SchId { get; set; }

    public int MedRepId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string TreatingAilment { get; set; } = null!;

    public string Medicine { get; set; } = null!;

    public TimeOnly MeetingStartTime { get; set; }

    public TimeOnly MeetingEndTime { get; set; }

    public DateOnly MeetingDate { get; set; }

    public string DoctorContact { get; set; } = null!;

    public virtual MedicalRep MedRep { get; set; } = null!;
}
