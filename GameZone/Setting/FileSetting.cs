using static System.Net.Mime.MediaTypeNames;

namespace GameZone.Setting
{
	public class FileSetting
	{
		public const string ImagePath = @"assets/Images/Games"; 
		public const string Extensions = ".jpg,.jpeg,.png";
		public const int AllowedFileSizeInMB = 10;
		public const int AllowedFileSizeInBytes = 10 * 1024*1024;


	}
}
