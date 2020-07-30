using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class UnitTypeCommandPromptScript : BaseBlueprintUnit, IUnitTypeCommandPromptScript
	{
		public override Constants.UnitType UnitType => Constants.UnitType.CommandPromptScript;

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
