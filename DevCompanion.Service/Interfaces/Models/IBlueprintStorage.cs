using Newtonsoft.Json;
using System;

namespace DevCompanion.Service
{
	/// <summary>
	/// Structure for storing blueprints.
	/// </summary>
	public interface IBlueprintStorage
	{
		/// <summary>
		/// Blueprint ID contained in Data
		/// </summary>
		Guid Id { get; }

		/// <summary>
		/// Encrypted JSON data containing all Blueprint details.
		/// </summary>
		string Data { get; set; }

		[JsonIgnore]
		Constants.StorageType StorageType { get; }

		[JsonIgnore]
		IBlueprint Blueprint { get; }

		[JsonIgnore]
		IBlueprintRegistryItem BlueprintRegistryItem { get; set; }
	}
}
