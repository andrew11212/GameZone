using GameZone.Data;
using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
	public class GamesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ICategoriesServices categoriesServices;
		private readonly IDevicesServices devicesServices;
		private readonly IGamesServices gamesServices;

		public GamesController(ApplicationDbContext context, ICategoriesServices categoriesServices, IDevicesServices devicesServices, IGamesServices gamesServices)
		{
			_context = context;
			this.categoriesServices = categoriesServices;
			this.devicesServices = devicesServices;
			this.gamesServices = gamesServices;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]

		public IActionResult Create()
		{
			CreateGameViewModel viewModel = new()
			{
				categories = categoriesServices.GetSelectList(),
				Devices = devicesServices.GetDevices(),
			};
			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.categories = categoriesServices.GetSelectList();

				viewModel.Devices = devicesServices.GetDevices();

				return View(viewModel);
			}
			 await gamesServices.Create(viewModel);

			return RedirectToAction(nameof(Index));
		}
	}
}
