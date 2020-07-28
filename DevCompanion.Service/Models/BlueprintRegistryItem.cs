using System;

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
	}
}
