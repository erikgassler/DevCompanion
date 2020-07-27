using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class AppSettingsLoader : IAppSettingsLoader
	{
		public AppSettingsLoader(IFileSystem fileSystem)
		{
			FileSystem = fileSystem;
		}

		/// <summary>
		/// Load app settings from local JSON file if exists.
		/// </summary>
		/// <returns></returns>
		public async Task<IAppSettings> LoadAppSettingsFromJSON()
		{
			try
			{
				string filePath = FileSystem.GetFullPath("AppSettings.json");
				if (!FileSystem.Exists(filePath)) { return null; }
				string json = await FileSystem.ReadAllTextAsync(filePath);
				return JsonConvert.DeserializeObject<AppSettings>(json);
			}
			catch(Exception _)
			{
				return null;
			}
		}

		private IFileSystem FileSystem { get; }
	}
}
