using DevCompanion.Desktop.UnitTypes;
using DevCompanion.Service;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevCompanion.Desktop.ContentPages
{
	/// <summary>
	/// Interaction logic for PageBlueprint.xaml
	/// </summary>
	public partial class PageBlueprint : BaseContentPage
	{
		public PageBlueprint(
			IDesktopService desktopService
			)
		{
			DesktopService = desktopService;
			Storage = DesktopService.ActiveBlueprintStorage;
			Blueprint = Storage.Blueprint;
			InitializeComponent();
			SetupPage();
			Blueprint = DesktopService.ActiveBlueprintStorage.Blueprint;
			RegisterEvents();
			DesktopService.UpdateStatus($"Blueprint Loaded!");
		}

		public override void UnloadForRemoval()
		{
			DesktopService.OnUpdatedBlueprint -= DesktopService_OnUpdatedBlueprint;
		}

		private void RegisterEvents()
		{
			DesktopService.OnUpdatedBlueprint += DesktopService_OnUpdatedBlueprint;
		}

		private void DesktopService_OnUpdatedBlueprint(object sender, IBlueprint e)
		{
			SetupUnits();
		}

		private void SetupPage()
		{
			SetupBlueprintName();
			BlueprintKey.Text = Storage.BlueprintRegistryItem.Key;
			FileLocation.Text = Storage.BlueprintRegistryItem.FilePath;
			SetupButtonBar();
			SetupUnits();
		}

		private void SetupButtonBar()
		{
			foreach(var (display, flag) in GetButtonBarList)
			{
				AddUnitTypeButtonToButtonBar(display, flag);
			}
			AddButtonToButtonBar("💾", "Save Blueprint", (sender, e) =>
			{
				DesktopService.SaveBlueprint();
			});

		}

		private void SetupUnits()
		{
			BlueprintUnits.Children.Clear();
			int controlsAdded = 0;
			foreach(BaseBlueprintUnit unit in Blueprint.Units)
			{
				controlsAdded += AddUnitToWorkspace(unit) ? 1 : 0;
			}
			if(controlsAdded == 0)
			{
				BlueprintUnits.Children.Add(new TextBlock()
				{
					Text = "This Blueprint doesn't contain any Units!"
				});
			}
		}

		private bool AddUnitToWorkspace(IBlueprintUnit unit)
		{
			ControlBaseUnit control = unit.UnitType switch
			{
				Constants.UnitType.AzureAppConfig => new ControlUnitTypeAzureAppConfig(unit),
				Constants.UnitType.AzureKeyVault => new ControlUnitTypeAzureKeyVault(unit),
				Constants.UnitType.Blueprint => new ControlUnitTypeBlueprint(unit),
				Constants.UnitType.CommandPromptScript => new ControlUnitTypeCommandPrompt(unit),
				Constants.UnitType.Documentation => new ControlUnitTypeDocumentation(unit),
				Constants.UnitType.EnvironmentVariable => new ControlUnitTypeEnvironmentVariable(unit),
				Constants.UnitType.PowerShellScript => new ControlUnitTypePowerShellScript(unit),
				Constants.UnitType.Workflow => new ControlUnitTypeWorkflow(unit),
				_ => null
			};
			if(control == null) { return false; }
			control.Margin = new Thickness(0, 0, 0, 10);
			BlueprintUnits.Children.Add(control);
			return true;
		}

		private (string display, Constants.UnitType flag)[] GetButtonBarList
		{
			get
			{
				List<(string display, Constants.UnitType flag)> list = new List<(string display, Constants.UnitType flag)>
				{
					("DC", Constants.UnitType.Documentation),
					("AC", Constants.UnitType.AzureAppConfig),
					("KV", Constants.UnitType.AzureKeyVault),
					("BP", Constants.UnitType.Blueprint),
					("CS", Constants.UnitType.CommandPromptScript),
					("EV", Constants.UnitType.EnvironmentVariable),
					("PS", Constants.UnitType.PowerShellScript),
					("WF", Constants.UnitType.Workflow)
				};
				return list.ToArray();
			}
		}

		private void AddUnitTypeButtonToButtonBar(string display, Constants.UnitType flag)
		{
			AddButtonToButtonBar(display, flag.DisplayName(), (sender, e) =>
			{
				DesktopService.AddUnit(flag);
			});
		}

		private void AddButtonToButtonBar(string display, string tooltip, RoutedEventHandler clickHandler)
		{
			Button button = new Button() { 
				Content = display,
				ToolTip = tooltip,
				Padding = new Thickness(5, 3, 5, 3),
				Margin = new Thickness(3, 0, 3, 0)
			};
			button.Click += clickHandler; ;
			ControlButtonBar.Children.Add(button);
		}

		private void SetupBlueprintName()
		{
			BlueprintName.DisplayText = Blueprint.Name;
			BlueprintName.ConfigureUpdatingStringSettings(() => Blueprint.Name, updatedName =>
			{
				Blueprint.Name = updatedName;
				BlueprintName.DisplayText = Blueprint.Name;
			});
		}

		private void CopyKeyToClipboard_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			try
			{
				Clipboard.SetText(BlueprintKey.Text);
				DesktopService.UpdateStatus($"`{BlueprintKey.Text}` has been copied to your clipboard!");
			}
			catch (Exception)
			{
				DesktopService.UpdateStatus("Failed to copy to Clipboard!");
			}
		}

		private IBlueprintStorage Storage { get; }
		private IBlueprint Blueprint { get; }
		private IDesktopService DesktopService { get; }
	}
}
