using System;

namespace DevCompanion.Service
{
	public class BlueprintStorage : IBlueprintStorage
	{
		/// <summary>
		/// Blueprint ID contained in Data
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Encrypted JSON data containing all Blueprint details.
		/// </summary>
		public string Data { get; set; }
	}
}
