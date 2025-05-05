using GameZone.Models;
using GameZone.ViewModel;

namespace GameZone.Services
{
	public interface IGamesServices
	{
		IEnumerable<Game> GetAll();
		Task Create (CreateGameViewModel gameViewModel);
		Task<Game?> FindByIdAsync(int id);
		Task<EditGameViewModel?> GetForEditAsync(int id);
		Task UpdateAsync(EditGameViewModel model);
	}
}
