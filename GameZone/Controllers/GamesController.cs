using GameZone.Data;
using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

			var gamelist = gamesServices.GetAll();
			return View(gamelist);
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
		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return BadRequest();
				
			var game = await gamesServices.FindByIdAsync(id.Value);
			
			if (game == null)
				return NotFound();
				
			return View(game);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return BadRequest();
				
			var model = await gamesServices.GetForEditAsync(id.Value);
			
			if (model == null)
				return NotFound();
				
			// Populate dropdown lists
			model.Categories = categoriesServices.GetSelectList();
			model.Devices = devicesServices.GetDevices();
			
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditGameViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = categoriesServices.GetSelectList();
				model.Devices = devicesServices.GetDevices();
				return View(model);
			}
			
			await gamesServices.UpdateAsync(model);
			
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			var game = await _context.Games.Include(g => g.Category).FirstOrDefaultAsync(m => m.id == id);
			if (game == null || id == 0)
			{
				return NotFound();
			}
			return View(game);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Delete(Game game)
		{
			_context.Games.Remove(game);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
