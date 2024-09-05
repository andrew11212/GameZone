using GameZone.Models;

namespace GameZone.Services
{
	public class DevicesServices:IDevicesServices
	{
		private readonly ApplicationDbContext context;

		public DevicesServices(ApplicationDbContext _context)
        {
			this.context = _context;
		}
		public IEnumerable<SelectListItem> GetDevices() 
		{
			return context.Devices
				.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
				.OrderBy(d => d.Text)
				.AsNoTracking()
				.ToList();
		}
    }
}
