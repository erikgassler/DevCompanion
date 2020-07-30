using System;
using System.IO;

namespace DevCompanion.Service
{
	public class BlueprintRegistryItem : IBlueprintRegistryItem
	{
		/// <summary>
		/// Blueprint ID
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Encryption key used to unlock Blueprint
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Name of Blueprint
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// File path location to Blueprint
		/// </summary>
		public string FilePath { get; set; }

		public void SetFilePath(string folderPath)
		{
			FilePath = Path.GetFullPath($"{folderPath}/{Id}.dcbp")
				.Replace(@"\\", "/")
				.Replace(@"\", "/");
		}
	}
}
