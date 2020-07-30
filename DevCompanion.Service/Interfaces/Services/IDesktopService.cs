using System;

namespace DevCompanion.Service
{
	public interface IDesktopService
	{
		event EventHandler<bool> OnCloseApplication;
		event EventHandler<string> OnStatusUpdated;
		event EventHandler<int> OnProgessUpdated;
		event EventHandler<IBlueprint> OnLoadedBlueprint;
		event EventHandler<IBlueprint> OnUpdatedBlueprint;
		event EventHandler<Constants.ContentPage> OnChangeContentPage;

		IBlueprintStorage ActiveBlueprintStorage { get; }
		void AddUnit(Constants.UnitType unitType);
		void UpdateStatus(string status);
		void UpdateProgress(int percent);
		void ChangeContentPage(Constants.ContentPage page);
		void StopDesktopServices();
		void CreateNewBlueprint();
		void OpenBlueprint(Guid blueprintId);
		void SaveBlueprint();
		void SyncBlueprint();
		void CloseApplication();
		void RunStartup();
	}
}
