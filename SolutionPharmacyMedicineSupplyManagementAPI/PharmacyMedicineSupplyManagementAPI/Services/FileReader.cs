namespace PharmacyMedicineSupplyManagementAPI.Services
{
	public class FileReader: IFileReader
	{
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public string ReadFile(string path)
		{
			return File.ReadAllText(path);
		}
	}
}
