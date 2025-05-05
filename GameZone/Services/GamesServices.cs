using GameZone.Services;
using GameZone.Setting;
using GameZone.ViewModel;
using Microsoft.EntityFrameworkCore;

public class GamesServices : IGamesServices
{
	private readonly ApplicationDbContext context;
	private readonly IWebHostEnvironment webHostEnvironment;
	private readonly string imagesPath;

	public GamesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
	{
		this.context = context;
		this.webHostEnvironment = webHostEnvironment;
		imagesPath = Path.Combine(webHostEnvironment.WebRootPath, FileSetting.ImagePath.Trim());
	}

	public IEnumerable<Game> GetAll()
	{
		var games = context.Games
			.Include(g=>g.Category)
			.Include(d=>d.Devises)
			.ThenInclude(d => d.Device)
			.AsNoTracking()
			.ToList();
		return games;
	}

	public async Task Create(CreateGameViewModel model)
	{
		var FileName = $"{Guid.NewGuid()}{Path.GetExtension(model.cover.FileName)}";
		var Filepath = Path.Combine(imagesPath, FileName);

		using (var stream = new FileStream(Filepath, FileMode.Create))
		{
			await model.cover.CopyToAsync(stream);
		}

		Game game = new Game
		{
			name = model.name,
			cover = FileName,
			CategoryID = model.CategoryID,
			description = model.description,
			Devises = model.SelectedDevices.Select(d => new GameDevise { DeviceId = d }).ToList(),
		};

		context.Games.Add(game);
		await context.SaveChangesAsync();
	}

	public async Task<Game?> FindByIdAsync(int id)
	{
		return await context.Games
			.Include(g => g.Category)
			.Include(g => g.Devises)
			.ThenInclude(d => d.Device)
			.FirstOrDefaultAsync(g => g.id == id);
	}

	public async Task<EditGameViewModel?> GetForEditAsync(int id)
	{
		var game = await context.Games
			.Include(g => g.Devises)
			.FirstOrDefaultAsync(g => g.id == id);

		if (game == null)
			return null;

		var selectedDeviceIds = game.Devises.Select(d => d.DeviceId).ToList();

		var model = new EditGameViewModel
		{
			Id = game.id,
			Name = game.name,
			Description = game.description,
			CategoryID = game.CategoryID,
			CurrentCover = game.cover,
			SelectedDevices = selectedDeviceIds
		};

		return model;
	}

	public async Task UpdateAsync(EditGameViewModel model)
	{
		var game = await context.Games
			.Include(g => g.Devises)
			.FirstOrDefaultAsync(g => g.id == model.Id);

		if (game == null)
			return;

		// Update basic properties
		game.name = model.Name;
		game.description = model.Description;
		game.CategoryID = model.CategoryID;

		// Update cover if changed
		if (model.Cover != null)
		{
			// Delete old image if exists
			if (!string.IsNullOrEmpty(game.cover))
			{
				var oldImagePath = Path.Combine(imagesPath, game.cover);
				if (File.Exists(oldImagePath))
					File.Delete(oldImagePath);
			}

			// Save new image
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
			var filePath = Path.Combine(imagesPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await model.Cover.CopyToAsync(stream);
			}

			game.cover = fileName;
		}

		// Update devices
		// Remove existing relationships
		game.Devises.Clear();

		// Add selected devices
		game.Devises = model.SelectedDevices.Select(id => new GameDevise { GameId = game.id, DeviceId = id }).ToList();

		await context.SaveChangesAsync();
	}
}
