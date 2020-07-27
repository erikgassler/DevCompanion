using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public interface IBlueprintUnit
	{
		Task<bool> RunProcessor();

		Task<bool> RunValidator();
	}
}
