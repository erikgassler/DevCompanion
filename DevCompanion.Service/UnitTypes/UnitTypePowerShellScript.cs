using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class UnitTypePowerShellScript : BaseBlueprintUnit, IUnitTypePowerShellScript
	{
		public override Constants.UnitType UnitType => Constants.UnitType.PowerShellScript;

		public override Constants.UnitFlag UnitFlag { get; set; } = Constants.UnitFlag.Documentation;

		public override Constants.UnitStage UnitState { get; set; } = Constants.UnitStage.Processor;

		public override async Task<bool> RunProcessor()
		{
			return false;
		}

		public override async Task<bool> RunValidator()
		{
			return false;
		}
	}
}
