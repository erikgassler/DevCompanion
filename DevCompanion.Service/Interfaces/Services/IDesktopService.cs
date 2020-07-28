namespace DevCompanion.Service.Interfaces
{
	public interface IDesktopService
	{
		void StopDesktopServices();
		void CreateNewBlueprint();
		void OpenBlueprint();
		void SaveBlueprint();
		void SyncBlueprint();
	}
}
