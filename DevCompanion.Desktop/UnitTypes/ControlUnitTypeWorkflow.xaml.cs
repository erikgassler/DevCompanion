using DevCompanion.Service;

namespace DevCompanion.Desktop.UnitTypes
{
	/// <summary>
	/// Interaction logic for ControlUnitTypeWorkflow.xaml
	/// </summary>
	public partial class ControlUnitTypeWorkflow : ControlBaseUnit
	{
		public ControlUnitTypeWorkflow(IBlueprintUnit unit)
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
