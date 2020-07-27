using System.Threading.Tasks;

namespace DevCompanion.Service
{
	/// <summary>
	/// Wrapper for OS file system interactions needed by services.
	/// </summary>
	public interface IFileSystem
	{
		string GetFullPath(string partialPath);
		bool Exists(string filePath);
		Task<string> ReadAllTextAsync(string filePath);
	}
}
