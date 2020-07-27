using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public interface IAppSettingsLoader
	{
		Task<IAppSettings> LoadAppSettingsFromJSON();
	}
}
