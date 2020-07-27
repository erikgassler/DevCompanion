namespace DevCompanion.Service
{
	public partial class Constants
	{
		public enum UnitStage
		{
			[About("This stage represents the main process of a unit that needs to be viewed or run.")]
			Processor = 1,
			[About("This stage represents a process that needs to run after the Processor and during health checkups to validate the unit success conditions have been met.")]
			Validator = 2
		}
	}
}
