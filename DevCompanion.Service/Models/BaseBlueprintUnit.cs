using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class BaseBlueprintUnit : IBlueprintUnit
	{
		public virtual Constants.UnitType UnitType { get; set; }

		public virtual Constants.UnitFlag UnitFlag { get; set; }

		public virtual Constants.UnitStage UnitState { get; set; }

		public virtual Task<bool> RunProcessor()
		{
			return Task.FromResult(false);
		}

		public virtual Task<bool> RunValidator()
		{
			return Task.FromResult(false);
		}
	}
}
