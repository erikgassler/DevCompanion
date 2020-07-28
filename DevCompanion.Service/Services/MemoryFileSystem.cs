using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	/// <summary>
	/// This is a placeholder IFileSystem to assure one is set by default for unit tests and Visual Studio tooling.
	/// UI Projects should replace this service with an appropriate FileSystem class to access OS services.
	/// </summary>
	public class MemoryFileSystem : IFileSystem
	{
		public bool Exists(string filePath)
		{
			return false;
		}

		public string GetFullPath(string partialPath)
		{
			return $"Memory/{partialPath}";
		}

		public Task<string> ReadAllTextAsync(string filePath)
		{
			return Task.FromResult("");
		}

		public void SetRegistryValue(string key, string value)
		{
			Registry[key] = value;
		}

		public bool TryGetRegistryValue(string key, out string value)
		{
			return Registry.TryGetValue(key, out value);
		}
		private Dictionary<string, string> Registry = new Dictionary<string, string>();
	}
}
