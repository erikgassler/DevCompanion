using DevCompanion.Service;
using System.IO;
using System.Threading.Tasks;

namespace DevCompanion.Desktop
{
	public class FileSystem : IFileSystem
	{
		public bool Exists(string filePath)
		{
			return File.Exists(filePath);
		}

		public string GetFullPath(string partialPath)
		{
			return Path.GetFullPath(partialPath);
		}

		public async Task<string> ReadAllTextAsync(string filePath)
		{
			return await File.ReadAllTextAsync(filePath);
		}
	}
}
