namespace DevCompanion.Service
{
	public interface IDesktopWindow
	{
		void UpdateStatus(string status);
		void UpdateProcessingProgress(int percent);
		void ChangeContentPage(Constants.ContentPage page);
		void CloseApplication();
	}
}
