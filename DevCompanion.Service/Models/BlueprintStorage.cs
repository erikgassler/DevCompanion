using Newtonsoft.Json;
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

		[JsonIgnore]
		public Constants.StorageType StorageType { get; set; }

		[JsonIgnore]
		public IBlueprint Blueprint { get; set; }

		[JsonIgnore]
		public IBlueprintRegistryItem BlueprintRegistryItem { get; set; }
	}
}
