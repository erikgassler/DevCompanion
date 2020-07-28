using System;

namespace DevCompanion.Service
{
	public interface IDesktopWindow
	{
		void UpdateStatus(string status);
		void UpdateProcessingProgress(int percent);
		void ChangeContentPage(Constants.ContentPage page);
		void CloseApplication();
		void LoadBlueprint(IBlueprint blueprint);
		void OpenBlueprint();
		void SaveBlueprint();
		void SyncBlueprint();
		event EventHandler<Constants.ContentPage> OnChangeContentPage;
		event EventHandler<IBlueprint> OnLoadedBlueprint;
	}
}
