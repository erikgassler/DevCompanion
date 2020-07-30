using System;

namespace DevCompanion.Service
{
	public class PlaceholderDesktopWindow : IDesktopWindow
	{
		public event EventHandler<Constants.ContentPage> OnChangeContentPage;
		public event EventHandler<IBlueprint> OnLoadedBlueprint;

		public void ChangeContentPage(Constants.ContentPage page)
		{
		}

		public void CloseApplication()
		{
		}

		public void LoadBlueprintIntoNewStorageContainer(IBlueprint blueprint)
		{
		}

		public void OpenBlueprint()
		{
		}

		public void SaveBlueprint()
		{
		}

		public void SyncBlueprint()
		{
		}

		public void UpdateProcessingProgress(int percent)
		{
		}

		public void UpdateStatus(string status)
		{
		}
	}
}
