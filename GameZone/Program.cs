using Azure.Messaging;
using GameZone.Data;
using GameZone.Services;
using Microsoft.EntityFrameworkCore;

namespace GameZone
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("DefualtConnection")
				?? throw new InvalidOperationException(message: "no Contection string was found");
			// Add services to the container.
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<ICategoriesServices,CategoriesServices>();
			builder.Services.AddScoped<IDevicesServices,DevicesServices>();
			builder.Services.AddScoped<IGamesServices,GamesServices>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
