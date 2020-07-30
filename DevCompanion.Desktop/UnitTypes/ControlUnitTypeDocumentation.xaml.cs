using DevCompanion.Service;
using System.Windows.Controls;

namespace DevCompanion.Desktop.UnitTypes
{
	/// <summary>
	/// Interaction logic for ControlUnitTypeDocumentation.xaml
	/// </summary>
	public partial class ControlUnitTypeDocumentation : ControlBaseUnit
	{
		public ControlUnitTypeDocumentation(IBlueprintUnit unit)
		{
			BlueprintUnit = unit;
			InitializeComponent();
		}

		IBlueprintUnit BlueprintUnit;
	}
}
