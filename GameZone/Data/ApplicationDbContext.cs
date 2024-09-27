using GameZone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameZone.Data
{
	public class ApplicationDbContext :IdentityDbContext
	{
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options) 
        {
            
        }

		public DbSet<Game> Games { get; set; }
		public DbSet<Category> Categories { get; set; }

		public DbSet<Device> Devices { get; set; }

		public DbSet<GameDevise> gameDevises { get; set; }
		public DbSet<Application_user> applicationUsers { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(new Category[]
			{
				new Category { Id = 1,Name="RPG" },
				new Category { Id = 2,Name="Sports" },
				new Category { Id = 3,Name="Action" },
				new Category { Id = 4,Name="Racing" },
				new Category { Id = 5,Name="fighting" },
				new Category { Id = 6,Name="Adventure" },

			});

			modelBuilder.Entity<Device>().HasData(new Device[] 
			{
				new Device {Id=1,Name="PlayStation",Icon="bi bi-playStation"},
				new Device {Id=2,Name="Xbox",Icon="bi bi-xbox"},
				new Device {Id=3,Name="Nintendo Switch",Icon="bi bi-nintendo-switch"},
				new Device {Id=4,Name="PC",Icon="bi bi-pc-display"}

			});
			modelBuilder.Entity<GameDevise>().HasKey(e => new { e.GameId, e.DeviceId });

			base.OnModelCreating(modelBuilder);

		}
	}
}
