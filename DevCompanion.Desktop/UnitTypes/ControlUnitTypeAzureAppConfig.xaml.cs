﻿using DevCompanion.Service;

namespace DevCompanion.Desktop.UnitTypes
{
	/// <summary>
	/// Interaction logic for ControlUnitTypeAzureAppConfig.xaml
	/// </summary>
	public partial class ControlUnitTypeAzureAppConfig : ControlBaseUnit
	{
		public ControlUnitTypeAzureAppConfig(IBlueprintUnit unit)
		{
			BlueprintUnit = unit;
			InitializeComponent();
		}

		private void SetupControl()
		{
			ControlToolbar.OnClickDelete += ControlToolbar_OnClickDelete;
		}

		private void ControlToolbar_OnClickDelete(object sender, int e)
		{
			// TODO - Remove control from list of units
		}

		IBlueprintUnit BlueprintUnit;
	}
}
