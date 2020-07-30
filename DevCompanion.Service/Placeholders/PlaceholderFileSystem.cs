using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	/// <summary>
	/// This is a placeholder IFileSystem to assure one is set by default for unit tests and Visual Studio tooling.
	/// UI Projects should replace this service with an appropriate FileSystem class to access OS services.
	/// </summary>
	public class PlaceholderFileSystem : IFileSystem
	{
		public bool Exists(string filePath)
		{
			return Files.ContainsKey(filePath);
		}

		public string GetFullPath(string partialPath)
		{
			return Path.GetFullPath(partialPath)
				.Replace(@"\\", "/")
				.Replace(@"\", "/");
		}

		public string GetDirectoryName(string path)
		{
			return Path.GetDirectoryName(path)
				.Replace(@"\\", "/")
				.Replace(@"\", "/");
		}

		public string ReadAllText(string filePath)
		{
			if (Files.ContainsKey(filePath))
			{
				return Files[filePath];
			}
			return "";
		}

		public void SetRegistryValue(string key, string value)
		{
			Registry[key] = value;
		}

		public bool TryGetRegistryValue(string key, out string value)
		{
			return Registry.TryGetValue(key, out value);
		}

		public bool IsValidDirectory(string path)
		{
			// just assume any test directory is valid
			return true;
		}

		public bool SaveFileToDirectory(string filePath, string json)
		{
			Files[filePath] = json;
			return true;
		}

		private Dictionary<string, string> Files { get; } = new Dictionary<string, string>();
		private Dictionary<string, string> Registry { get; } = new Dictionary<string, string>();
	}
}
