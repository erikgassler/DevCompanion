using DevCompanion.Service;

namespace DevCompanion.Desktop.UnitTypes
{
	/// <summary>
	/// Interaction logic for ControlUnitTypeCommandPrompt.xaml
	/// </summary>
	public partial class ControlUnitTypeCommandPrompt : ControlBaseUnit
	{
		public ControlUnitTypeCommandPrompt(IBlueprintUnit unit)
		{
			BlueprintUnit = unit;
			InitializeComponent();
			SetupControl();
		}

		private void SetupControl()
		{
			ControlToolbar.OnClickDelete += ControlToolbar_OnClickDelete;
		}

		private void ControlToolbar_OnClickDelete(object sender, int e)
		{
			// TODO - Remove control from list of units
		}

		private IBlueprintUnit BlueprintUnit { get; }
	}
}
