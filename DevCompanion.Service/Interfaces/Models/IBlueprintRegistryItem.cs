using System;

namespace DevCompanion.Service
{
	public interface IBlueprintRegistryItem
	{
		/// <summary>
		/// Blueprint ID
		/// </summary>
		Guid Id { get; set; }

		/// <summary>
		/// Encryption key used to unlock Blueprint
		/// </summary>
		string Key { get; set; }

		/// <summary>
		/// Name of Blueprint
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// File path location to Blueprint
		/// </summary>
		string FilePath { get; set; }

		void SetFilePath(string folderPath);
	}
}
