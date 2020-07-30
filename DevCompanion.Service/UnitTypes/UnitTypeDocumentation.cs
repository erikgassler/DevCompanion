using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class UnitTypeDocumentation : BaseBlueprintUnit, IUnitTypeDocumentation
	{
		public override Constants.UnitType UnitType => Constants.UnitType.Documentation;

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
