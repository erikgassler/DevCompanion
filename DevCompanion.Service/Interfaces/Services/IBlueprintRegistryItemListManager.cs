using System;

namespace DevCompanion.Service
{
	public interface IBlueprintRegistryItemListManager
	{
		void PrependRegistryItemToList(BlueprintRegistryItem item);
		BlueprintRegistryItem GetRegistryItem(Guid blueprintId);
		void RemoveRegistryItem(Guid blueprintId);
	}
}
