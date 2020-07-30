using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class UnitTypeWorkflow : BaseBlueprintUnit, IUnitTypeWorkflow
	{
		public override Constants.UnitType UnitType => Constants.UnitType.Workflow;

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
