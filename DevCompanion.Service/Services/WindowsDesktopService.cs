using DevCompanion.Service.Interfaces;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class WindowsDesktopService : IDesktopService
	{
		public WindowsDesktopService()
		{

		}

		/// <summary>
		/// Called once when the application has started and is opening the main window.
		/// </summary>
		/// <param name="window"></param>
		public void InitializeDesktop(IDesktopWindow window)
		{
			window.UpdateStatus("Initializing services!");
			DesktopWindow = window;
			SetStartupContent();
			RunServices();
			window.UpdateStatus("Services initialized!");
		}

		/// <summary>
		/// Called once when a trigger requests closing the application.
		/// </summary>
		public void StopDesktopServices()
		{
			ReadyToCloseApp = true;
			Stopwatch waitTime = Stopwatch.StartNew();
			// Allow services to stop gracefully before allowing app to finish closing - unless it takes to long, then just let the app die.
			while(waitTime.ElapsedMilliseconds < MaxMillisecondsToWaitForCloase && ServicesAreRunning)
			{
				Thread.Sleep(100);
			}
			ServiceTask = null;
		}

		public void CreateNewBlueprint()
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

		private void SetStartupContent()
		{
			DesktopWindow.ChangeContentPage(Constants.ContentPage.FirstStartup);
		}

		private void RunServices()
		{
			ServicesAreRunning = true;
			ServiceTask = Task.Run(() => {
				while (!ReadyToCloseApp)
				{
					IncrementProgressBar();
					Thread.Sleep(ProcessingFrameIntervalInMilliseconds);
				}
				ServicesAreRunning = false;
			});
		}

		private int Progress = 0;
		private void IncrementProgressBar()
		{
			Progress += 1;
			if(Progress > 100)
			{
				Progress = 0;
			}
			DesktopWindow.UpdateProcessingProgress(Progress);
		}

		private int ProcessingFrameIntervalInMilliseconds = 16;
		private const int MaxMillisecondsToWaitForCloase = 5000;
		private Task ServiceTask { get; set; }
		private bool ReadyToCloseApp { get; set; } = false;
		private bool ServicesAreRunning { get; set; } = false;
		private IDesktopWindow DesktopWindow { get; set; }
	}
}
