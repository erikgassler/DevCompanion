using DevCompanion.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace DevCompanion.BuildTests.Services
{
	public class UTBlueprintRegistryItemListManager : MockHelpers
	{
		[Fact]
		public void VerifyLatestSavesAndLoadsGetMovedToTopOfList()
		{
			/**
			 * IBlueprintRegistryItemListManager will update the BlueprintList variable in AppSettings based on the commands given.
			 * This appSettings/fileSystem mock setup will simulate our updates to the system registry, which is where this data is stored in the actual app.
			 * */
			IAppSettings appSettings = GetAppSettingWithMocks(out IFileSystem fileSystem);
			IBlueprintRegistryItemListManager manager = new BlueprintRegistryItemListManager(appSettings);
			List<BlueprintRegistryItem> items = new List<BlueprintRegistryItem>();
			foreach(string nameKey in new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"})
			{
				string name = $"Item {nameKey}";
				// add new item to our test list
				items.Add(GetRegistryItem(name, $"KeyPass {nameKey}"));
				// prepend item we just created to AppSettings list through the manager
				manager.PrependRegistryItemToList(items[^1]);
				// Assure new item was prepended in AppSettings.BlueprintList
				Assert.Equal(name, appSettings.BlueprintList[0].Name);
			}
			int listLength = appSettings.BlueprintList.Count;
			// get item id from middle of list
			Guid itemId = items[5].Id;
			// use manager to retrieve item from AppSettings.BlueprintList
			BlueprintRegistryItem item = manager.GetRegistryItem(itemId);
			// prepend item to start of list
			manager.PrependRegistryItemToList(item);
			// Assure item was prepended
			Assert.Equal(item.Id, appSettings.BlueprintList[0].Id);
			// Assure prepending existing item did not increase list size
			Assert.Equal(listLength, appSettings.BlueprintList.Count);
			// removing item from list
			manager.RemoveRegistryItem(item.Id);
			// Assure count decreased by 1
			Assert.Equal(listLength - 1, appSettings.BlueprintList.Count);
			// Assure can no longer retrieve item we just removed
			Assert.Null(manager.GetRegistryItem(item.Id));
		}

		private BlueprintRegistryItem GetRegistryItem(string name, string password)
		{
			return new BlueprintRegistryItem()
			{
				Id = Guid.NewGuid(),
				Key = password,
				Name = name
			};
		}
	}
}
