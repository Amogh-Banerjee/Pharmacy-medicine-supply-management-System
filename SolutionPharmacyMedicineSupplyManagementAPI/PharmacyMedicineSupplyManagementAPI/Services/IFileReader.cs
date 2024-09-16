namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public interface IFileReader
	{
		bool FileExists(string path);
		string ReadFile(string path);
	}
}
