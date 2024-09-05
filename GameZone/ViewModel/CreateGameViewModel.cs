

using GameZone.Attriputes;
using GameZone.Setting;

namespace GameZone.ViewModel
{
	public class CreateGameViewModel
	{
		
		[MaxLength(length: 500)]
		public string name { get; set; } = string.Empty;

		[Display (Name ="Category")]
		public int CategoryID { get; set; }

		public IEnumerable<SelectListItem> categories { get; set; }= Enumerable.Empty<SelectListItem>();

		[Display(Name = "Devices")]

		public List<int> SelectedDevices {  get; set; }= new List<int>();

		public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
		
		[MaxLength(length: 2500)]
		public string description { get; set; } = string.Empty;

		[AllowedExtensions(FileSetting.Extensions)]
		[MaxFileSize(FileSetting.AllowedFileSizeInBytes)]
		public IFormFile cover { get; set; } = default!;
	}
}
