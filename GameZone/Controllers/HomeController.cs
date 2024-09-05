using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGamesServices IGamesServices;

		public HomeController(IGamesServices _IGamesServices)
		{
			IGamesServices = _IGamesServices;
		}

		public IActionResult Index()
		{
			var games = IGamesServices.GetAll();
			return View(games);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
