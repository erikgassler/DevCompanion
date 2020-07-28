namespace DevCompanion.Service.Interfaces
{
	public interface IDesktopService
	{
		void InitializeDesktop(IDesktopWindow window);
		void StopDesktopServices();
		void CreateNewBlueprint();
		void OpenBlueprint();
		void SaveBlueprint();
		void SyncBlueprint();
	}
}
