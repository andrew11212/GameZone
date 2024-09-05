using GameZone.ViewModel;

namespace GameZone.Services
{
	public interface IGamesServices
	{
		IEnumerable<Game> GetAll();
		Task Create (CreateGameViewModel gameViewModel);
	}
}
