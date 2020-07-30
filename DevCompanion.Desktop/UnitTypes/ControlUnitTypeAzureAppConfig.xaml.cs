using DevCompanion.Service;
using System.Windows.Controls;

namespace DevCompanion.Desktop.UnitTypes
{
	/// <summary>
	/// Interaction logic for ControlUnitTypeAzureAppConfig.xaml
	/// </summary>
	public partial class ControlUnitTypeAzureAppConfig : UserControl
	{
		public ControlUnitTypeAzureAppConfig(IBlueprintUnit unit)
		{
			BlueprintUnit = unit;
			InitializeComponent();
		}

		IBlueprintUnit BlueprintUnit;
	}
}
