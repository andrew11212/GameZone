namespace GameZone.Models
{
	public class GameDevise
	{

		public Game Game { get; set; } = default!;
		public int GameId { get; set; }

		public Device Device { get; set; } = default!;
		public int DeviceId {  get; set; }
	}
}
