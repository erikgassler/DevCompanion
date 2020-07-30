using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class BaseBlueprintUnit : IBlueprintUnit
	{
		public Constants.UnitType UnitType { get; set; }

		public Constants.UnitFlag UnitFlag { get; set; }

		public Constants.UnitStage UnitState { get; set; }

		public Task<bool> RunProcessor()
		{
			return Task.FromResult(false);
		}

		public Task<bool> RunValidator()
		{
			return Task.FromResult(false);
		}
	}
}
