using CsvHelper;
using PharmacyMedicineSupplyManagementAPI.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PharmacyMedicineSupplyManagementAPI.Repositories
{
	public class MedicalRepresentativeScheduleRepo: IMedicalRepresentativeScheduleRepo
	{
		private readonly MedDbContext _context;

		public MedicalRepresentativeScheduleRepo(MedDbContext context)
		{
			_context = context;
		}

		public List<Doctor> GetAllDoctors()
		{
			var doctors = new List<Doctor>();
			using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "DoctorDetails.csv")))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				doctors = csv.GetRecords<Doctor>().ToList();
			}
			return doctors;
		}

		public List<MedicalRep> GetAllMedicalReps()
		{
			return _context.MedicalReps.ToList();
		}

		public void AddSchedule(RepSchedule schedule)
		{
			_context.RepSchedules.Add(schedule);
			_context.SaveChanges();
		}

	}
}
