using System;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	/// <summary>
	/// Wrapper for OS file system interactions needed by services.
	/// </summary>
	public interface IFileSystem
	{
		bool IsValidDirectory(string path);
		bool SaveFileToDirectory(string filePath, string json);
		string GetFullPath(string partialPath);
		string GetDirectoryName(string path);
		bool TryGetRegistryValue(string key, out string value);
		void SetRegistryValue(string key, string value);
		Task<string> ReadAllTextAsync(string filePath);
	}
}
