using GameZone.Attriputes;
using GameZone.Setting;

namespace GameZone.ViewModel
{
    public class EditGameViewModel
    {
        public int Id { get; set; }
        
        [MaxLength(length: 500)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Devices")]
        public List<int> SelectedDevices { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
        
        [MaxLength(length: 2500)]
        public string Description { get; set; } = string.Empty;

        public string? CurrentCover { get; set; }

        [AllowedExtensions(FileSetting.Extensions)]
        [MaxFileSize(FileSetting.AllowedFileSizeInBytes)]
        public IFormFile? Cover { get; set; }
    }
} 