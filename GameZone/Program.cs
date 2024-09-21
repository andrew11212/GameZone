using Azure.Messaging;
using GameZone.Data;
using GameZone.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

			builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<ICategoriesServices,CategoriesServices>();
			builder.Services.AddScoped<IDevicesServices,DevicesServices>();
			builder.Services.AddScoped<IGamesServices,GamesServices>();
			builder.Services.AddRazorPages();//add razor page to use identity

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

			app.UseAuthentication();
			app.UseAuthorization();
			app.MapRazorPages();//add middle ware to razor page for identity
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");


			app.Run();
		}
	}
}
