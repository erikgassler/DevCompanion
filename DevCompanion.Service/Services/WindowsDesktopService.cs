using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class WindowsDesktopService : IDesktopService
	{
		public WindowsDesktopService(
			IServiceProvider serviceProvider,
			IAppSettings appSettings
			)
		{
			AppSettings = appSettings;
			ServiceProvider = serviceProvider;
			LoaderService = ServiceProvider.GetService<IBlueprintLoaderService>();
			ListManager = ServiceProvider.GetService<IBlueprintRegistryItemListManager>();
			RunServices();
		}

		/// <summary>
		/// Called by IDesktopWindow service when Window has finished startup processing.
		/// </summary>
		public void RunStartup()
		{
			SetStartupContent();
		}

		public IBlueprintStorage ActiveBlueprintStorage { get; private set; }
		public event EventHandler<IBlueprint> OnLoadedBlueprint;
		public event EventHandler<IBlueprint> OnUpdatedBlueprint;
		public event EventHandler<string> OnStatusUpdated;
		public event EventHandler<int> OnProgessUpdated;
		public event EventHandler<Constants.ContentPage> OnChangeContentPage;
		public event EventHandler<bool> OnCloseApplication;

		public void UpdateStatus(string status)
		{
			OnStatusUpdated?.Invoke(this, status);
		}
		private object LockPageSwap { get; } = new object();

		public void UpdateProgress(int percent)
		{
			OnProgessUpdated?.Invoke(this, percent);
		}

		public void ChangeContentPage(Constants.ContentPage page)
		{
			lock (LockPageSwap)
			{
				if (page == CurrentPage) { return; }
				CurrentPage = page;
				OnChangeContentPage?.Invoke(this, CurrentPage);
			}
		}
		private Constants.ContentPage CurrentPage { get; set; }

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
			IBlueprint blueprint = ServiceProvider.GetService<IBlueprint>();
			ActiveBlueprintStorage = new BlueprintStorage()
			{
				Id = blueprint.Id,
				StorageType = Constants.StorageType.UnSaved,
				Blueprint = blueprint,
				BlueprintRegistryItem = new BlueprintRegistryItem()
				{
					Id = blueprint.Id,
					Key = AppSettings.LocalEncryptionKey,
					Name = blueprint.Name
				}
			};
			ActiveBlueprintStorage.BlueprintRegistryItem.SetFilePath(AppSettings.DefaultSaveFolder);
			ChangeContentPage(Constants.ContentPage.CreateBlueprint);
			OnLoadedBlueprint?.Invoke(this, blueprint);
		}

		public void OpenBlueprint(Guid blueprintId)
		{
			ChangeContentPage(Constants.ContentPage.Loading);
			IBlueprintStorage storage = LoaderService.LoadBlueprint(blueprintId).GetAwaiter().GetResult();
			if(storage == null)
			{
				UpdateStatus($"Failed to open Blueprint!");
				ChangeContentPage(Constants.ContentPage.Error);
				return;
			}
			ActiveBlueprintStorage = storage;
			ChangeContentPage(Constants.ContentPage.ViewBlueprint);
			ListManager.PrependRegistryItemToList((BlueprintRegistryItem)ActiveBlueprintStorage.BlueprintRegistryItem);
			OnLoadedBlueprint?.Invoke(this, ActiveBlueprintStorage.Blueprint);
		}

		public void SaveBlueprint()
		{
			if(ActiveBlueprintStorage == null) { return; }
			if (!LoaderService.SaveBlueprint(ActiveBlueprintStorage).GetAwaiter().GetResult())
			{
				UpdateStatus($"Failed to save Blueprint {ActiveBlueprintStorage.BlueprintRegistryItem.Name}!");
				return;
			}
			ChangeContentPage(Constants.ContentPage.ViewBlueprint);
			ListManager.PrependRegistryItemToList((BlueprintRegistryItem)ActiveBlueprintStorage.BlueprintRegistryItem);
			UpdateStatus($"Saved {ActiveBlueprintStorage.BlueprintRegistryItem.Name}!");
		}

		public void AddUnit(Constants.UnitType unitType)
		{
			switch (unitType)
			{
				case Constants.UnitType.AzureAppConfig:
					ActiveBlueprintStorage.Blueprint.Units.Add(new UnitTypeAzureAppConfig());
					break;
				case Constants.UnitType.AzureKeyVault:
					ActiveBlueprintStorage.Blueprint.Units.Add(new UnitTypeAzureKeyVault());
					break;
				case Constants.UnitType.Blueprint:
					ActiveBlueprintStorage.Blueprint.Units.Add(new UnitTypeBlueprint());
					break;
				case Constants.UnitType.CommandPromptScript:
					ActiveBlueprintStorage.Blueprint.Units.Add(new UnitTypeCommandPromptScript());
					break;
				case Constants.UnitType.Documentation:
					ActiveBlueprintStorage.Blueprint.Units.Add(new UnitTypeDocumentation());
					break;
				case Constants.UnitType.EnvironmentVariable:
					ActiveBlueprintStorage.Blueprint.Units.Add(new UnitTypeEnvironmentVariable());
					break;
				case Constants.UnitType.PowerShellScript:
					ActiveBlueprintStorage.Blueprint.Units.Add(new UnitTypePowerShellScript());
					break;
				case Constants.UnitType.Workflow:
					ActiveBlueprintStorage.Blueprint.Units.Add(new UnitTypeWorkflow());
					break;
				default:
					UpdateStatus($"Whoops, unrecognized Unit type!");
					return;
			}
			OnUpdatedBlueprint?.Invoke(this, ActiveBlueprintStorage.Blueprint);
		}

		public void SyncBlueprint()
		{
			UpdateStatus("Syncing Blueprint not yet supported!");
		}

		private void SetStartupContent()
		{
			if(AppSettings.BlueprintList.Count == 0)
			{
				ChangeContentPage(Constants.ContentPage.FirstStartup);
				return;
			}
			ChangeContentPage(Constants.ContentPage.Loading);
			OpenBlueprint(AppSettings.BlueprintList[0].Id);
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
			UpdateProgress(Progress);
		}

		public void CloseApplication()
		{
			StopDesktopServices();
			OnCloseApplication?.Invoke(this, true);
		}

		private int ProcessingFrameIntervalInMilliseconds = 16;
		private const int MaxMillisecondsToWaitForCloase = 5000;
		private Task ServiceTask { get; set; }
		private bool ReadyToCloseApp { get; set; } = false;
		private bool ServicesAreRunning { get; set; } = false;
		private IAppSettings AppSettings { get; }
		private IServiceProvider ServiceProvider { get; }
		private IBlueprintLoaderService LoaderService { get; }
		private IBlueprintRegistryItemListManager ListManager { get; }
	}
}
