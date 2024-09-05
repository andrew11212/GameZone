using GameZone.Services;

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
		var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.cover.FileName)}";
		var path = Path.Combine(imagesPath, coverName);

		// Ensure the directory exists
		var directory = Path.GetDirectoryName(path);
		if (!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}

		using (var stream = new FileStream(path, FileMode.Create))
		{
			await model.cover.CopyToAsync(stream);
		}

		Game game = new Game
		{
			name = model.name,
			cover = coverName,
			CategoryID = model.CategoryID,
			description = model.description,
			Devises = model.SelectedDevices.Select(d => new GameDevise { DeviceId = d }).ToList(),
		};

		context.Games.Add(game);
		await context.SaveChangesAsync();
	}
}
