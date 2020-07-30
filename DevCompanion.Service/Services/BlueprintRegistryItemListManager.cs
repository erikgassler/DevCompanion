using System;

namespace DevCompanion.Service
{
	public class BlueprintRegistryItemListManager : IBlueprintRegistryItemListManager
	{
		public BlueprintRegistryItemListManager(
			IAppSettings appSettings
			)
		{
			AppSettings = appSettings;
		}

		public void PrependRegistryItemToList(BlueprintRegistryItem item)
		{
			if(item == null) { return; }
			RemoveRegistryItem(item.Id);
			AppSettings.BlueprintList.Insert(0, item);
		}

		public void RemoveRegistryItem(Guid blueprintId)
		{
			BlueprintRegistryItem item = GetRegistryItem(blueprintId);
			if(item == null) { return; }
			AppSettings.BlueprintList.Remove(item);
		}

		public BlueprintRegistryItem GetRegistryItem(Guid blueprintId)
		{
			foreach(BlueprintRegistryItem item in AppSettings.BlueprintList)
			{
				if(item.Id == blueprintId)
				{
					return item;
				}
			}
			return null;
		}

		private IAppSettings AppSettings { get; }
	}
}
