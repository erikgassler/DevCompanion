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
	}
}
