using System;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public interface IBlueprintLoaderService
	{
		Task<bool> SaveBlueprint(IBlueprintStorage blueprintStorage);

		Task<BlueprintStorage> LoadBlueprint(Guid blueprintId);
	}
}
