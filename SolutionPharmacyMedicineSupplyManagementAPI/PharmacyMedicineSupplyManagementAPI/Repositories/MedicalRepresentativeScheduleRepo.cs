using CsvHelper;
using PharmacyMedicineSupplyManagementAPI.Models;
using PharmacyMedicineSupplyManagementAPI.Services;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace PharmacyMedicineSupplyManagementAPI.Repositories
{
	public class MedicalRepresentativeScheduleRepo: IMedicalRepresentativeScheduleRepo
	{
		private readonly MedDbContext _context;
		private readonly IFileReader _fileReader;

		public MedicalRepresentativeScheduleRepo(MedDbContext context, IFileReader fileReader)
		{
			_context = context;
			_fileReader = fileReader;
		}

		public List<Doctor> GetAllDoctors()
		{
			var doctors = new List<Doctor>();
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "DoctorDetails.csv");

            if (!_fileReader.FileExists(filePath))
            {
				throw new FileNotFoundException($"The file at {filePath} was not found.");
			}

			try
			{
				using (var reader = new StringReader(_fileReader.ReadFile(filePath)))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					doctors = csv.GetRecords<Doctor>().ToList();
				}
			}
			catch(Exception ex)
			{
				throw new Exception("An error occurred while reading the CSV file.", ex);
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
